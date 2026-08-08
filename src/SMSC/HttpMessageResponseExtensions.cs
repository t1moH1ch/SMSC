namespace SMSC;

public static class HttpSmsResponseExtensions
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    /// <summary>
    /// Расширение для сериализации ответа от сервера. Поддерживается только JSON формат ответа сервера.
    /// </summary>
    public static async Task<HttpSmsResponse> CreateHttpSmsResponseAsync(this HttpResponseMessage response, SmsConfiguration config)
    {
        var content = await HttpApiErrorHelper.ReadJsonContentAsync(response, config);
        HttpApiErrorHelper.ThrowIfError(content);

        return JsonSerializer.Deserialize<HttpSmsResponse>(content, JsonOptions)
            ?? throw new InvalidOperationException("Something went wrong. Response read stopped.");
    }

    /// <summary>
    /// Расширение для сериализации ответа статуса.
    /// </summary>
    public static async Task<IEnumerable<HttpSmsStatusResponse>> CreateHttpSmsStatusResponseAsync(this HttpResponseMessage response, SmsStatusConfiguration config)
    {
        var content = await HttpApiErrorHelper.ReadJsonContentAsync(response, config);

        if (content.Contains("error_code", StringComparison.CurrentCultureIgnoreCase))
        {
            var error = JsonSerializer.Deserialize<HttpSmsErrorResponse>(content)
                ?? throw new InvalidOperationException("Failed to parse SMSC error response.");

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

        if (!content.TrimStart().StartsWith('['))
        {
            var json = JsonSerializer.Deserialize<HttpSmsStatusResponse>(content, JsonOptions);
            return json is null
                ? throw new InvalidOperationException("Something went wrong. Response read stopped.")
                : new List<HttpSmsStatusResponse> { json };
        }

        return JsonSerializer.Deserialize<List<HttpSmsStatusResponse>>(content, JsonOptions)
            ?? throw new InvalidOperationException("Something went wrong. Response read stopped.");
    }

    /// <summary>
    /// Расширение для сериализации ответа баланса.
    /// </summary>
    public static async Task<HttpSmsBalanceResponse> CreateHttpSmsBalanceResponseAsync(this HttpResponseMessage response, SmsBalanceConfiguration config)
    {
        var content = await HttpApiErrorHelper.ReadJsonContentAsync(response, config);

        if (content.Contains("error_code", StringComparison.CurrentCultureIgnoreCase))
        {
            var error = JsonSerializer.Deserialize<HttpSmsErrorResponse>(content)
                ?? throw new InvalidOperationException("Failed to parse SMSC error response.");

            throw error.ErrorCode switch
            {
                1 => new HttpSmsParametersException(),
                2 => new HttpSmsWrongCredentialsException(),
                4 => new HttpSmsIPAddressBlockException(),
                9 => new HttpSmsTooManyConcurrentRequestsException(),
                _ => new InvalidCastException("Unknown server exception.")
            };
        }

        return JsonSerializer.Deserialize<HttpSmsBalanceResponse>(content, JsonOptions)
            ?? throw new InvalidOperationException("Something went wrong. Response read stopped.");
    }

    public static async Task<HttpWaitCallResponse> CreateHttpWaitCallResponseAsync(this HttpResponseMessage response, WaitCallConfiguration config)
    {
        var content = await HttpApiErrorHelper.ReadJsonContentAsync(response, config);
        HttpApiErrorHelper.ThrowIfError(content);
        return JsonSerializer.Deserialize<HttpWaitCallResponse>(content, JsonOptions)
            ?? throw new InvalidOperationException("Something went wrong. Response read stopped.");
    }

    public static Task<IEnumerable<HttpContactResponse>> CreateHttpContactResponseAsync(this HttpResponseMessage response, ContactsConfiguration config)
        => DeserializeOneOrManyAsync<HttpContactResponse>(response, config);

    public static Task<IEnumerable<HttpJobsResponse>> CreateHttpJobsResponseAsync(this HttpResponseMessage response, JobsConfiguration config)
        => DeserializeOneOrManyAsync<HttpJobsResponse>(response, config);

    public static Task<IEnumerable<HttpHistoryMessageResponse>> CreateHttpHistoryResponseAsync(this HttpResponseMessage response, HistoryConfiguration config)
        => DeserializeOneOrManyAsync<HttpHistoryMessageResponse>(response, config);

    public static Task<IEnumerable<HttpSenderResponse>> CreateHttpSenderResponseAsync(this HttpResponseMessage response, SendersConfiguration config)
        => DeserializeOneOrManyAsync<HttpSenderResponse>(response, config);

    public static Task<IEnumerable<HttpUsersResponse>> CreateHttpUsersResponseAsync(this HttpResponseMessage response, UsersConfiguration config)
        => DeserializeOneOrManyAsync<HttpUsersResponse>(response, config);

    public static Task<IEnumerable<HttpReceivePhoneResponse>> CreateHttpReceivePhoneResponseAsync(this HttpResponseMessage response, ReceivePhonesConfiguration config)
        => DeserializeOneOrManyAsync<HttpReceivePhoneResponse>(response, config);

    public static Task<IEnumerable<HttpDownloadResponse>> CreateHttpDownloadResponseAsync(this HttpResponseMessage response, DownloadsConfiguration config)
        => DeserializeOneOrManyAsync<HttpDownloadResponse>(response, config);

    private static async Task<IEnumerable<T>> DeserializeOneOrManyAsync<T>(HttpResponseMessage response, IConfiguration config)
    {
        var content = await HttpApiErrorHelper.ReadJsonContentAsync(response, config);
        HttpApiErrorHelper.ThrowIfError(content);

        if (!content.TrimStart().StartsWith('['))
        {
            var single = JsonSerializer.Deserialize<T>(content, JsonOptions);
            return single is null
                ? throw new InvalidOperationException("Something went wrong. Response read stopped.")
                : new List<T> { single };
        }

        return JsonSerializer.Deserialize<List<T>>(content, JsonOptions)
            ?? throw new InvalidOperationException("Something went wrong. Response read stopped.");
    }
}
