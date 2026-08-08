namespace SMSC.Http;

/// <summary>
/// Выделенные номера для приёма сообщений (<seealso href="https://smsc.ru/api/http/miscellaneous/receive/">receive_phones.php</seealso>).
/// </summary>
public class HttpReceivePhones(
    ProviderConfiguration configuration) : HttpAbstract(configuration)
{
    private ReceivePhonesConfiguration? _config => SmsConfiguration as ReceivePhonesConfiguration;

    protected override string SiteAddressCommand { get; } = "receive_phones.php";

    public virtual Task<IEnumerable<HttpReceivePhoneResponse>> GetAsync(CancellationToken ct = default)
        => ExecuteAsync(new ReceivePhonesConfiguration { Operation = ReceivePhonesOperation.Get }, ct);

    public virtual Task<IEnumerable<HttpReceivePhoneResponse>> BuyAsync(string phone, CancellationToken ct = default)
        => ExecuteAsync(new ReceivePhonesConfiguration { Operation = ReceivePhonesOperation.Buy, Phone = phone }, ct);

    public virtual Task<IEnumerable<HttpReceivePhoneResponse>> ChangeAsync(string phone, bool noProlong, CancellationToken ct = default)
        => ExecuteAsync(new ReceivePhonesConfiguration { Operation = ReceivePhonesOperation.Change, Phone = phone, NoProlong = noProlong }, ct);

    public virtual async Task<IEnumerable<HttpReceivePhoneResponse>> ExecuteAsync(ReceivePhonesConfiguration config, CancellationToken cancellationToken = default)
    {
        SmsConfiguration = config;
        return await (await SendRequest(CreateRequest(), cancellationToken))
            .CreateHttpReceivePhoneResponseAsync(config);
    }

    protected override Dictionary<string, string> GetQueryDictionary()
    {
        CheckSmsConfiguration();
        var c = _config!;
        var @params = new Dictionary<string, string>();

        switch (c.Operation)
        {
            case ReceivePhonesOperation.Get: @params["get"] = "1"; break;
            case ReceivePhonesOperation.Buy: @params["buy"] = "1"; break;
            case ReceivePhonesOperation.Change: @params["chg"] = "1"; break;
            default: throw new NotImplementedException($"Unknown receive phones operation {c.Operation}");
        }

        if (!string.IsNullOrEmpty(c.Phone)) @params["phone"] = c.Phone;
        if (c.Operation == ReceivePhonesOperation.Change)
            @params["noprolong"] = c.NoProlong ? "1" : "0";

        AddResponseFormat(@params);
        AddCharset(@params);
        return @params;
    }
}
