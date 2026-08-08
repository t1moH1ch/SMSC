namespace SMSC.Http;

public abstract class HttpAbstract(
    ProviderConfiguration configuration)
{
    protected const int MaxMessageLength = 1000;
    protected const int MaxQueryStringLength = 2000;
    private const int MaxFailoverAttempts = 5;

    protected ProviderConfiguration ProviderConfig = configuration ?? throw new ArgumentNullException(nameof(configuration));
    protected readonly HttpClient _httpClient = new();

    /// <summary>
    /// Для разделения телефонных номеров в списке
    /// </summary>
    protected const string PhonesDelimeter = ";";
    /// <summary>
    /// Название endpoint для отправки команд
    /// </summary>
    protected abstract string SiteAddressCommand { get; }

    /// <summary>
    /// Отправка запроса серверу в формате JSON (только для <c>send.php</c> / <c>rest/send/</c>).
    /// </summary>
    public virtual bool UseJsonRequestFormat { get; set; }
    /// <summary>
    /// Конфигурация для отправляемого сообщения
    /// </summary>
    protected virtual IConfiguration? SmsConfiguration { get; set; }

    /// <summary>
    /// Создание тела запроса в соответствии с настройками
    /// </summary>
    /// <param name="addClientsMessage">Пользовательская функция для phones/mes/list и т.п.</param>
    /// <returns>Настроенный объект класса <see cref="HttpRequestMessage"/></returns>
    protected virtual HttpRequestMessage CreateRequest(Action<Dictionary<string, string>>? addClientsMessage = null)
    {
        CheckSmsConfiguration();

        var payload = CreateCredentialDictionary();
        ThrowHelper.ThrowIfNull(payload, nameof(payload));
        addClientsMessage?.Invoke(payload);

        foreach (var pair in GetQueryDictionary())
            payload[pair.Key] = pair.Value;

        if (payload.Count == 0)
            throw new HttpSmsParametersException($"{nameof(payload)} can`t be empty.");

        var requestUri = CreateQueryString(payload);
        return CreateRequestMessage(requestUri, payload);
    }

    /// <summary>
    /// Настройка строки запроса
    /// </summary>
    /// <returns>Список параметров в виде словаря (ключ-значение)</returns>
    protected abstract Dictionary<string, string> GetQueryDictionary();

    /// <summary>
    /// Создание списка параметров для подключения через логин и пароль либо через ApiKey
    /// </summary>
    protected virtual Dictionary<string, string> CreateCredentialDictionary()
    {
        var paramsDict = new Dictionary<string, string>();

        if (ProviderConfig.ApiKey is not null)
        {
            ThrowHelper.ThrowIfNullOrEmpty(ProviderConfig.ApiKey);
            paramsDict.Add("apikey", ProviderConfig.ApiKey);
        }
        else
        {
            ThrowHelper.ThrowIfNullOrEmpty(ProviderConfig.Password);
            ThrowHelper.ThrowIfNullOrEmpty(ProviderConfig.Login);
            paramsDict.Add("login", ProviderConfig.Login);
            paramsDict.Add("psw", ProviderConfig.Password);
        }

        return paramsDict;
    }

    /// <summary>
    /// Создание URI запроса со всеми параметрами в query string
    /// </summary>
    protected virtual Uri CreateQueryString(Dictionary<string, string> payload)
    {
        var requestUri = new Uri(ProviderConfig.SmscApiAddress.ToString() + SiteAddressCommand);
        return new Uri($"{requestUri}?" +
            string.Join("&", payload.Select(p => $"{p.Key}={p.Value}")));
    }

    /// <summary>
    /// Создание объекта <see cref="HttpRequestMessage"/> для отправки запроса
    /// </summary>
    protected virtual HttpRequestMessage CreateRequestMessage(Uri requestUri, Dictionary<string, string> payload)
    {
        if (UseJsonRequestFormat)
        {
            if (!SupportsJsonRequestFormat)
                throw new NotSupportedException("JSON request format is only supported for send.php (https://smsc.ru/rest/send/).");

            return new HttpRequestMessage(HttpMethod.Post, ProviderConfig.JsonSmscApiAddress)
            {
                Content = JsonContent.Create(payload)
            };
        }

        var method = GetRequestMethod(requestUri, payload);
        if (method == HttpMethod.Post && ShouldPostParametersInBody(payload))
        {
            var baseUri = new Uri(ProviderConfig.SmscApiAddress.ToString() + SiteAddressCommand);
            return new HttpRequestMessage(HttpMethod.Post, baseUri)
            {
                Content = new FormUrlEncodedContent(payload)
            };
        }

        return new HttpRequestMessage(method, requestUri);
    }

    /// <summary>
    /// Поддерживает ли текущий endpoint JSON REST (<c>rest/send/</c>).
    /// </summary>
    protected virtual bool SupportsJsonRequestFormat => false;

    /// <summary>
    /// Нужно ли отправлять параметры в теле POST (без multipart-файлов).
    /// </summary>
    protected virtual bool ShouldPostParametersInBody(Dictionary<string, string> payload) => false;

    /// <summary>
    /// Выбор HTTP-метода
    /// </summary>
    protected virtual HttpMethod GetRequestMethod(Uri requestUri, Dictionary<string, string> payload)
        => requestUri.OriginalString.Length > MaxQueryStringLength ? HttpMethod.Post : HttpMethod.Get;

    /// <summary>
    /// Отправка запроса с failover на www1..www5.smsc.ru при пустом ответе
    /// </summary>
    protected virtual async Task<HttpResponseMessage> SendRequest(HttpRequestMessage message, CancellationToken cancellationToken = default)
    {
        var baseUri = message.RequestUri ?? throw new InvalidOperationException("Request URI is required.");
        var method = message.Method;
        byte[]? bodyBytes = null;
        string? contentType = null;

        if (message.Content is not null)
        {
            bodyBytes = await message.Content.ReadAsByteArrayAsync(
#if NET5_0_OR_GREATER
                cancellationToken
#endif
            );
            contentType = message.Content.Headers.ContentType?.ToString();
        }

        message.Dispose();

        HttpResponseMessage? lastResponse = null;
        for (var attempt = 0; attempt <= MaxFailoverAttempts; attempt++)
        {
            var uri = attempt == 0 ? baseUri : WithFailoverHost(baseUri, attempt);
            using var request = new HttpRequestMessage(method, uri);
            if (bodyBytes is not null)
            {
                request.Content = new ByteArrayContent(bodyBytes);
                if (!string.IsNullOrEmpty(contentType))
                    request.Content.Headers.TryAddWithoutValidation("Content-Type", contentType);
            }

            lastResponse?.Dispose();
            lastResponse = await _httpClient.SendAsync(request, cancellationToken);
            var content = await lastResponse.Content.ReadAsStringAsync(
#if NET5_0_OR_GREATER
                cancellationToken
#endif
            );

            if (!string.IsNullOrEmpty(content))
            {
                var mediaType = lastResponse.Content.Headers.ContentType?.MediaType ?? "text/plain";
                lastResponse.Content = new StringContent(content, Encoding.UTF8, mediaType);
                return lastResponse;
            }
        }

        throw new InvalidOperationException("Empty response from SMSC after host failover.");
    }

    private static Uri WithFailoverHost(Uri uri, int attempt)
    {
        var builder = new UriBuilder(uri)
        {
            Host = $"www{attempt}.smsc.ru"
        };
        return builder.Uri;
    }

    /// <summary>
    /// Проверка корректности конфигурации
    /// </summary>
    protected virtual void CheckSmsConfiguration() => ThrowHelper.ThrowIfNull(SmsConfiguration, nameof(SmsConfiguration));

    /// <summary>
    /// Добавление признака формата возвращаемого сообщения
    /// </summary>
    protected void AddResponseFormat(Dictionary<string, string> @params)
    {
        if (SmsConfiguration!.ResponseFormat != ResponseFormat.Default)
        {
            switch (SmsConfiguration.ResponseFormat)
            {
                case ResponseFormat.Digits: @params.Add("fmt", "1"); break;
                case ResponseFormat.XML: @params.Add("fmt", "2"); break;
                case ResponseFormat.Json: @params.Add("fmt", "3"); break;
            }
        }
    }

    /// <summary>
    /// Настройка кодировки
    /// </summary>
    protected void AddCharset(Dictionary<string, string> @params)
    {
        switch (SmsConfiguration!.Charset)
        {
            case Charset.WIN1251: return;
            case Charset.UTF8: @params.Add("charset", "utf-8"); break;
            case Charset.KOI8R: @params.Add("charset", "koi8-r"); break;
        }
    }

    /// <summary>
    /// Проверка максимальной длины сообщения (1000 символов по документации SMSC)
    /// </summary>
    protected static void EnsureMessageLength(string? message)
    {
        if (message is not null && message.Length > MaxMessageLength)
            throw new MessageMaxLengthException(MaxMessageLength);
    }
}
