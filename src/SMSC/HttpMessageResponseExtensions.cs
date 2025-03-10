#if NET_FRAMEWORK_AND_NET_STANDARD
using Newtonsoft.Json;
#endif

namespace SMSC;

/// <summary>
/// Расширение для создания структурированных ответов сервера
/// </summary>
public static class HttpSmsResponseExtensions
{
    /// <summary>
    /// Расширение для сериализации ответа от сервера. Поддерживается только <i>JSON</i> формат ответа сервера.
    /// </summary>
    /// <param name="response">Ответ сервера</param>
    /// <param name="config">Настройки запроса</param>
    /// <returns></returns>
    /// <exception cref="HttpSmsParametersException"></exception>
    /// <exception cref="HttpSmsWrongCredentialsException"></exception>
    /// <exception cref="HttpSmsFundLimitException"></exception>
    /// <exception cref="HttpSmsIPAddressBlockException"></exception>
    /// <exception cref="HttpSmsDateFormatException"></exception>
    /// <exception cref="HttpSmsMessageForbiddenException"></exception>
    /// <exception cref="HttpSmsPhoneNumberFormatException"></exception>
    /// <exception cref="HttpSmsMessageDeliveringException"></exception>
    /// <exception cref="HttpSmsTooManyConcurrentRequestsException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<HttpSmsResponse> CreateHttpSmsResponseAsync(this HttpResponseMessage response, SmsConfiguration config)
    {
        var content = await EnsureCorrectResponseType(response, config);

#if NET_CORE_APP_AND_NET_STANDARD_21
        if (content.Contains("error_code", StringComparison.CurrentCultureIgnoreCase))
#else
        if (content.Contains("error_code"))
#endif
        {
#if NET_CORE_APP
            var error = await response.Content.ReadFromJsonAsync<HttpSmsErrorResponse>();
#else
            var errorString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<HttpSmsErrorResponse>(errorString);
#endif
#if NET_CORE_APP_8
            ArgumentNullException.ThrowIfNull(error);
#else
            if (error is null)
                throw new ArgumentNullException(nameof(error));
#endif

            throw error.ErrorCode switch
            {
                1 => new HttpSmsParametersException(),
                2 => new HttpSmsWrongCredentialsException(),
                3 => new HttpSmsFundLimitException(),
                4 => new HttpSmsIPAddressBlockException(),
                5 => new HttpSmsDateFormatException(),
                6 => new HttpSmsMessageForbiddenException(),
                7 => new HttpSmsPhoneNumberFormatException(),
                8 => new HttpSmsMessageDeliveringException(),
                9 => new HttpSmsTooManyConcurrentRequestsException(),
                _ => new InvalidCastException("Unknown server exception."),
            };
        }
#if NET_CORE_APP
        var json = await response.Content.ReadFromJsonAsync<HttpSmsResponse>(new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        });
#else
        var jsonString = await response.Content.ReadAsStringAsync();
        var json = JsonConvert.DeserializeObject<HttpSmsResponse>(jsonString, new JsonSerializerSettings
        {
            FloatParseHandling = FloatParseHandling.Decimal
        });
#endif
        return json is null ? throw new InvalidOperationException("Something went wrong. Response read stopped.") : json;
    }
    /// <summary>
    /// Расширение для сериализации ответа от сервера. Поддерживается только <i>JSON</i> формат ответа сервера.
    /// </summary>
    /// <param name="response">Ответ сервера</param>
    /// <param name="config">Настройки запроса</param>
    /// <returns></returns>
    /// <exception cref="HttpSmsParametersException"></exception>
    /// <exception cref="HttpSmsWrongCredentialsException"></exception>
    /// <exception cref="HttpSmsIPAddressBlockException"></exception>
    /// <exception cref="HttpSmsStatusDeleteException"></exception>
    /// <exception cref="HttpSmsTooManyConcurrentRequestsException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<IEnumerable<HttpSmsStatusResponse>> CreateHttpSmsStatusResponseAsync(this HttpResponseMessage response, SmsStatusConfiguration config)
    {
        var content = await EnsureCorrectResponseType(response, config);
#if NET_CORE_APP_AND_NET_STANDARD_21
        if (content.Contains("error_code", StringComparison.CurrentCultureIgnoreCase))
#else
        if (content.Contains("error_code"))
#endif
        {
#if NET_CORE_APP
            var error = await response.Content.ReadFromJsonAsync<HttpSmsErrorResponse>();
#else
            var errorString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<HttpSmsErrorResponse>(errorString);
#endif
#if NET_CORE_APP_8
            ArgumentNullException.ThrowIfNull(error);
#else
            if (error is null)
                throw new ArgumentNullException(nameof(error));
#endif

            throw error.ErrorCode switch
            {
                1 => new HttpSmsParametersException(),
                2 => new HttpSmsWrongCredentialsException(),
                4 => new HttpSmsIPAddressBlockException(),
                5 => new HttpSmsStatusDeleteException(),
                9 => new HttpSmsTooManyConcurrentRequestsException(),
                _ => new InvalidCastException("Unknown server exception."),
            };
        }

#if NET_CORE_APP
        if (!content.StartsWith('['))
#else
        if (!content.StartsWith("["))
#endif
        {
#if NET_CORE_APP
            var json = await response.Content.ReadFromJsonAsync<HttpSmsStatusResponse>(new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            });
#else
            var jsonString = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<HttpSmsStatusResponse>(jsonString, new JsonSerializerSettings
            {
                FloatParseHandling = FloatParseHandling.Decimal
            });
#endif
            return json is null ? throw new InvalidOperationException("Something went wrong. Response read stopped.") : new List<HttpSmsStatusResponse>() { json };
        }
        else
        {
#if NET_CORE_APP
            var json = await response.Content.ReadFromJsonAsync<List<HttpSmsStatusResponse>>(new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            });
#else
            var jsonString = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<List<HttpSmsStatusResponse>>(jsonString, new JsonSerializerSettings
            {
                FloatParseHandling = FloatParseHandling.Decimal
            });
#endif
            return json is null ? throw new InvalidOperationException("Something went wrong. Response read stopped.") : json;
        }
    }
    /// <summary>
    /// Расширение для сериализации ответа от сервера. Поддерживается только <i>JSON</i> формат ответа сервера.
    /// </summary>
    /// <param name="response">Ответ сервера</param>
    /// <param name="config">Настройки запроса</param>
    /// <returns></returns>
    /// <exception cref="HttpSmsParametersException"></exception>
    /// <exception cref="HttpSmsWrongCredentialsException"></exception>
    /// <exception cref="HttpSmsIPAddressBlockException"></exception>
    /// <exception cref="HttpSmsStatusDeleteException"></exception>
    /// <exception cref="HttpSmsTooManyConcurrentRequestsException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<HttpSmsBalanceResponse> CreateHttpSmsBalanceResponseAsync(this HttpResponseMessage response, SmsBalanceConfiguration config)
    {
        var content = await EnsureCorrectResponseType(response, config);
#if NET_CORE_APP_AND_NET_STANDARD_21
        if (content.Contains("error_code", StringComparison.CurrentCultureIgnoreCase))
#else
        if (content.Contains("error_code"))
#endif
        {
#if NET_CORE_APP
            var error = await response.Content.ReadFromJsonAsync<HttpSmsErrorResponse>();
#else
            var errorString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<HttpSmsErrorResponse>(errorString);
#endif
#if NET_CORE_APP
            ArgumentNullException.ThrowIfNull(error);
#else
            if (error is null)
                throw new ArgumentNullException(nameof(error));
#endif

            throw error.ErrorCode switch
            {
                1 => new HttpSmsParametersException(),
                2 => new HttpSmsWrongCredentialsException(),
                4 => new HttpSmsIPAddressBlockException(),
                9 => new HttpSmsTooManyConcurrentRequestsException(),
                _ => new InvalidCastException("Unknown server exception.")
            };
        }

#if NET_CORE_APP
        var json = await response.Content.ReadFromJsonAsync<HttpSmsBalanceResponse>(new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        });
#else
        var jsonString = await response.Content.ReadAsStringAsync();
        var json = JsonConvert.DeserializeObject<HttpSmsBalanceResponse>(jsonString, new JsonSerializerSettings
        {
            FloatParseHandling = FloatParseHandling.Decimal
        });
#endif
        return json is null ? throw new InvalidOperationException("Something went wrong. Response read stopped.") : json;
    }

    /// <summary>
    /// Проверяем формат ответа сервера
    /// </summary>
    /// <param name="response">Oтвет от сервера</param>
    /// <param name="config">Список параметров запроса</param>
    /// <returns>Строка ответа, возвращаемая сервером</returns>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="ArgumentNullException"/>
    private static async Task<string> EnsureCorrectResponseType(HttpResponseMessage response, IConfiguration config)
    {
        response.EnsureSuccessStatusCode();

#if NET_CORE_APP
        //Для ответов в кодировках, отлиных от UTF-8, зарегистрируем другие
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif

        var content = await response.Content.ReadAsStringAsync();
#if NET_CORE_APP_8
        ArgumentException.ThrowIfNullOrEmpty(content);
#else
        if (string.IsNullOrEmpty(content))
            throw new ArgumentNullException(nameof(content));
#endif
        if (config.ResponseFormat != ResponseFormat.Json)
            throw new NotSupportedException("Only JSON format is supported for server response.");

        return content;
    }
}