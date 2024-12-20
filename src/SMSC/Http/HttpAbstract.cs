namespace SMSC.Http;

/// <summary>
/// Абстракция для классов, работающих с API smsc.ru
/// </summary>
/// <param name="configuration"></param>
public abstract class HttpAbstract(
    ProviderConfiguration configuration)
{
    /// <summary>
    /// Настройки подключения
    /// </summary>
    protected ProviderConfiguration ProviderConfig = configuration ?? throw new ArgumentNullException(nameof(configuration));
    /// <summary>
    /// Клиент web-request
    /// </summary>
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
    /// Отправка запроса серверу в формате JSON.
    /// </summary>
    public virtual bool UseJsonRequestFormat { get; set; }
    /// <summary>
    /// Конфигурация для отправляемого сообщения
    /// </summary>
    protected virtual IConfiguration? SmsConfiguration { get; set; }

    /// <summary>
    /// Создание тела запроса в соостветствии с настройками
    /// </summary>
    /// <param name="addClientsMessage">Пользовательская функция</param>
    /// <returns>Настроенный объект класса <see cref="HttpRequestMessage"/></returns>
    protected virtual HttpRequestMessage CreateRequest(Action<Dictionary<string, string>>? addClientsMessage = null)
    {
        CheckSmsConfiguration();

        var paramsDict = CreateCreadentialDictionary();
        ArgumentNullException.ThrowIfNull(paramsDict, nameof(paramsDict));
        if (addClientsMessage is not null)
            addClientsMessage(paramsDict);

        var @params = GetQueryDictionary();
        if (!@params.Any())
            throw new HttpSmsParametersException($"{nameof(@params)} can`t be empty.");

        var requestUri = CreateQueryString(paramsDict);
        return CreateRequestMessage(requestUri, @params);
    }
    /// <summary>
    /// Настройка строки запроса
    /// </summary>
    /// <returns>Список параметров в виде словаря (ключ-значение)</returns>
    protected abstract Dictionary<string, string> GetQueryDictionary();
    /// <summary>
    /// Создание списка параметров для подключения через логин и пароль либо через ApiKey
    /// </summary>
    /// <returns>Список параметров для подключения</returns>
    protected virtual Dictionary<string, string>? CreateCreadentialDictionary()
    {
        var paramsDict = new Dictionary<string, string>();

        if (ProviderConfig.ApiKey is not null)
        {
            ArgumentException.ThrowIfNullOrEmpty(ProviderConfig.ApiKey);
            paramsDict.Add("apikey", ProviderConfig.ApiKey);
        }
        else
        {
            ArgumentException.ThrowIfNullOrEmpty(ProviderConfig.Password);
            ArgumentException.ThrowIfNullOrEmpty(ProviderConfig.Login);
            paramsDict.Add("login", ProviderConfig.Login);
            paramsDict.Add("psw", ProviderConfig.Password);
        }

        return paramsDict;
    }
    /// <summary>
    /// Создание строки запроса методом GET
    /// </summary>
    /// <param name="paramsDict">Список параметров запроса</param>
    /// <returns>Строка запроса</returns>
    /// <exception cref="HttpSmsParametersException"></exception>
    protected virtual Uri CreateQueryString(Dictionary<string, string> paramsDict)
    {
        var @params = GetQueryDictionary();
        if (!@params.Any())
            throw new HttpSmsParametersException($"{nameof(@params)} can`t be empty.");

        var requestUri = new Uri(ProviderConfig.SmscApiAddress.ToString() + SiteAddressCommand);
        return new Uri($"{requestUri}?" +
            $"{string.Join("&", paramsDict.Select(p => $"{p.Key}={p.Value}"))}&" +
            $"{string.Join("&", @params.Select(p => $"{p.Key}={p.Value}"))}");
    }
    /// <summary>
    /// Создание объекта <see cref="HttpRequestMessage"/> для отправки запроса
    /// </summary>
    /// <param name="requestUri">Строка запроса</param>
    /// <param name="params">Список параметров запроса</param>
    /// <returns>Объект класса <see cref="HttpRequestMessage"/></returns>
    protected virtual HttpRequestMessage CreateRequestMessage(Uri requestUri, Dictionary<string, string> @params)
    {
        if (!UseJsonRequestFormat)
            return new HttpRequestMessage(GetRequestMethod(), requestUri);

        return new HttpRequestMessage(HttpMethod.Post, new Uri(ProviderConfig.JsonSmscApiAddress.ToString()))
        {
            Content = JsonContent.Create(@params)
        };
    }
    /// <summary>
    /// При использовании одного из параметров (отправка email или прикрепление файла к сообщению)
    /// запрос должен отправляться методом POST
    /// </summary>
    /// <returns>
    /// <see langword="true"/> - необходимо использовать метод POST<br/>
    /// <see langword="false"/> - необходимо использовать метод GET
    /// </returns>
    /// <exception cref="ArgumentNullException"/>
    protected virtual HttpMethod GetRequestMethod() => HttpMethod.Get;

    /// <summary>
    /// Отправка запроса
    /// </summary>
    /// <param name="message">Подготовленный запрос</param>
    /// <param name="cancellationToken">Токен прерывания</param>
    /// <returns>Результат выполнения запроса в формате <see cref="HttpResponseMessage"/></returns>
    protected virtual async Task<HttpResponseMessage> SendRequest(HttpRequestMessage message, CancellationToken cancellationToken = default)
        => await _httpClient.SendAsync(message, cancellationToken);

    /// <summary>
    /// Проверка корректности конфигурации
    /// </summary>
    protected virtual void CheckSmsConfiguration() => ArgumentNullException.ThrowIfNull(SmsConfiguration, nameof(SmsConfiguration));
    /// <summary>
    /// Добавление признака формата возвращаемого сообщения
    /// </summary>
    /// <param name="params">Массив с параметрами соощения</param>
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
    /// <paramref name="params">Список параметров сообщения</paramref>
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
}
