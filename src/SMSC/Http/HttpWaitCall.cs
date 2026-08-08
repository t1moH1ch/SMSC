namespace SMSC.Http;

/// <summary>
/// Подтверждение номера телефона звонком (<seealso href="https://smsc.ru/api/http/miscellaneous/waitcall/">wait_call.php</seealso>).
/// </summary>
public class HttpWaitCall(
    ProviderConfiguration configuration,
    HttpClient? httpClient = null) : HttpAbstract(configuration, httpClient)
{
    protected override string SiteAddressCommand { get; } = "wait_call.php";

    /// <summary>
    /// Запрос номера для подтверждения звонком.
    /// </summary>
    /// <param name="phone">Номер абонента, который нужно подтвердить.</param>
    public virtual async Task<HttpWaitCallResponse> RequestAsync(string phone, WaitCallConfiguration? config = null, CancellationToken cancellationToken = default)
    {
        ThrowHelper.ThrowIfNullOrEmpty(phone, nameof(phone));
        SmsConfiguration = config ?? new WaitCallConfiguration();

        return await (await SendRequest(CreateRequest(p => p.Add("phone", phone)), cancellationToken))
            .CreateHttpWaitCallResponseAsync((WaitCallConfiguration)SmsConfiguration);
    }

    protected override Dictionary<string, string> GetQueryDictionary()
    {
        CheckSmsConfiguration();
        var @params = new Dictionary<string, string>();
        AddResponseFormat(@params);
        AddCharset(@params);
        return @params;
    }
}
