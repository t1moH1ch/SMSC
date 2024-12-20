﻿namespace SMSC;

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

        if (content.Contains("error_code", StringComparison.CurrentCultureIgnoreCase))
        {
            var error = await response.Content.ReadFromJsonAsync<HttpSmsErrorResponse>();
            ArgumentNullException.ThrowIfNull(error);

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

        var json = await response.Content.ReadFromJsonAsync<HttpSmsResponse>(new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        });
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

        if (content.Contains("error_code", StringComparison.CurrentCultureIgnoreCase))
        {
            var error = await response.Content.ReadFromJsonAsync<HttpSmsErrorResponse>();
            ArgumentNullException.ThrowIfNull(error);

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

        if (!content.StartsWith('['))
        {
            var json = await response.Content.ReadFromJsonAsync<HttpSmsStatusResponse>(new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            });
            return json is null ? throw new InvalidOperationException("Something went wrong. Response read stopped.") : new List<HttpSmsStatusResponse>() { json };
        }
        else
        {
            var json = await response.Content.ReadFromJsonAsync<List<HttpSmsStatusResponse>>(new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            });
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

        if (content.Contains("error_code", StringComparison.CurrentCultureIgnoreCase))
        {
            var error = await response.Content.ReadFromJsonAsync<HttpSmsErrorResponse>();
            ArgumentNullException.ThrowIfNull(error);

            throw error.ErrorCode switch
            {
                1 => new HttpSmsParametersException(),
                2 => new HttpSmsWrongCredentialsException(),
                4 => new HttpSmsIPAddressBlockException(),
                9 => new HttpSmsTooManyConcurrentRequestsException(),
                _ => new InvalidCastException("Unknown server exception.")
            };
        }

        var json = await response.Content.ReadFromJsonAsync<HttpSmsBalanceResponse>(new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        });
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

        //Для ответов в кодировках, отлиных от UTF-8, зарегистрируем другие
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var content = await response.Content.ReadAsStringAsync();
        ArgumentException.ThrowIfNullOrEmpty(content);
        if (config.ResponseFormat != ResponseFormat.Json)
            throw new NotSupportedException("Only JSON format is supported for server response.");

        return content;
    }
}