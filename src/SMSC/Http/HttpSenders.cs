namespace SMSC.Http;

/// <summary>
/// Управление именами отправителей (<seealso href="https://smsc.ru/api/http/senders/">senders.php</seealso>).
/// </summary>
public class HttpSenders(
    ProviderConfiguration configuration) : HttpAbstract(configuration)
{
    private SendersConfiguration? _config => SmsConfiguration as SendersConfiguration;

    protected override string SiteAddressCommand { get; } = "senders.php";

    public virtual Task<IEnumerable<HttpSenderResponse>> GetAsync(SendersConfiguration? config = null, CancellationToken ct = default)
    {
        config ??= new SendersConfiguration();
        config.Operation = SendersOperation.Get;
        return ExecuteAsync(config, ct);
    }

    public virtual Task<IEnumerable<HttpSenderResponse>> AddAsync(string sender, string? comment = null, CancellationToken ct = default)
        => ExecuteAsync(new SendersConfiguration { Operation = SendersOperation.Add, Sender = sender, Comment = comment }, ct);

    public virtual Task<IEnumerable<HttpSenderResponse>> ChangeAsync(string sender, string? comment = null, string? id = null, CancellationToken ct = default)
        => ExecuteAsync(new SendersConfiguration { Operation = SendersOperation.Change, Sender = sender, Comment = comment, Id = id }, ct);

    public virtual Task<IEnumerable<HttpSenderResponse>> DeleteAsync(string? sender = null, string? id = null, CancellationToken ct = default)
        => ExecuteAsync(new SendersConfiguration { Operation = SendersOperation.Delete, Sender = sender, Id = id }, ct);

    public virtual Task<IEnumerable<HttpSenderResponse>> SendCodeAsync(string sender, CancellationToken ct = default)
        => ExecuteAsync(new SendersConfiguration { Operation = SendersOperation.SendCode, Sender = sender }, ct);

    public virtual Task<IEnumerable<HttpSenderResponse>> CheckCodeAsync(string sender, string code, CancellationToken ct = default)
        => ExecuteAsync(new SendersConfiguration { Operation = SendersOperation.CheckCode, Sender = sender, Code = code }, ct);

    public virtual async Task<IEnumerable<HttpSenderResponse>> ExecuteAsync(SendersConfiguration config, CancellationToken cancellationToken = default)
    {
        SmsConfiguration = config;
        return await (await SendRequest(CreateRequest(), cancellationToken))
            .CreateHttpSenderResponseAsync(config);
    }

    protected override Dictionary<string, string> GetQueryDictionary()
    {
        CheckSmsConfiguration();
        var c = _config!;
        var @params = new Dictionary<string, string>();

        switch (c.Operation)
        {
            case SendersOperation.Get: @params["get"] = "1"; break;
            case SendersOperation.Add: @params["add"] = "1"; break;
            case SendersOperation.Change: @params["chg"] = "1"; break;
            case SendersOperation.Delete: @params["del"] = "1"; break;
            case SendersOperation.SendCode: @params["send_code"] = "1"; break;
            case SendersOperation.CheckCode: @params["check_code"] = "1"; break;
            default: throw new NotImplementedException($"Unknown senders operation {c.Operation}");
        }

        if (!string.IsNullOrEmpty(c.Sender)) @params["sender"] = c.Sender;
        if (!string.IsNullOrEmpty(c.Id)) @params["id"] = c.Id;
        if (!string.IsNullOrEmpty(c.Comment)) @params["cmt"] = c.Comment;
        if (!string.IsNullOrEmpty(c.Code)) @params["code"] = c.Code;
        if (!string.IsNullOrEmpty(c.Site)) @params["site"] = c.Site;
        if (!string.IsNullOrEmpty(c.SiteName)) @params["site_name"] = c.SiteName;
        if (!string.IsNullOrEmpty(c.User)) @params["user"] = c.User;
        if (!string.IsNullOrEmpty(c.Name)) @params["name"] = c.Name;
        if (c.All) @params["all"] = "1";
        if (c.Operators) @params["operators"] = "1";
        if (c.Mail) @params["mail"] = "1";
        if (c.Count is not null) @params["cnt"] = c.Count.Value.ToString();
        if (!string.IsNullOrEmpty(c.PrevId)) @params["prev_id"] = c.PrevId;
        if (!string.IsNullOrEmpty(c.FromDate)) @params["from_date"] = c.FromDate;

        AddResponseFormat(@params);
        AddCharset(@params);
        return @params;
    }
}
