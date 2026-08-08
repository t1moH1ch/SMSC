namespace SMSC.Http;

/// <summary>
/// Управление рассылками (<seealso href="https://smsc.ru/api/http/jobs/">jobs.php</seealso>).
/// </summary>
public class HttpJobs(
    ProviderConfiguration configuration) : HttpAbstract(configuration)
{
    private JobsConfiguration? _config => SmsConfiguration as JobsConfiguration;

    protected override string SiteAddressCommand { get; } = "jobs.php";

    public virtual Task<IEnumerable<HttpJobsResponse>> AddAsync(JobsConfiguration config, CancellationToken ct = default)
    {
        config.Operation = JobsOperation.Add;
        return ExecuteAsync(config, ct);
    }

    public virtual Task<IEnumerable<HttpJobsResponse>> DeleteAsync(string id, CancellationToken ct = default)
        => ExecuteAsync(new JobsConfiguration { Operation = JobsOperation.Delete, Id = id }, ct);

    public virtual Task<IEnumerable<HttpJobsResponse>> CancelAsync(string id, CancellationToken ct = default)
        => ExecuteAsync(new JobsConfiguration { Operation = JobsOperation.Cancel, Id = id }, ct);

    public virtual Task<IEnumerable<HttpJobsResponse>> GetAsync(string id, CancellationToken ct = default)
        => ExecuteAsync(new JobsConfiguration { Operation = JobsOperation.Get, Id = id }, ct);

    public virtual Task<IEnumerable<HttpJobsResponse>> GetAllAsync(string? start = null, string? end = null, CancellationToken ct = default)
        => ExecuteAsync(new JobsConfiguration { Operation = JobsOperation.GetAll, Start = start, End = end }, ct);

    public virtual async Task<IEnumerable<HttpJobsResponse>> ExecuteAsync(JobsConfiguration config, CancellationToken cancellationToken = default)
    {
        if (config.Operation == JobsOperation.Add)
            EnsureMessageLength(config.Message);

        SmsConfiguration = config;
        return await (await SendRequest(CreateRequest(), cancellationToken))
            .CreateHttpJobsResponseAsync(config);
    }

    protected override Dictionary<string, string> GetQueryDictionary()
    {
        CheckSmsConfiguration();
        var c = _config!;
        var @params = new Dictionary<string, string>();

        switch (c.Operation)
        {
            case JobsOperation.Add: @params["add"] = "1"; break;
            case JobsOperation.Delete: @params["del"] = "1"; break;
            case JobsOperation.Cancel: @params["cancel"] = "1"; break;
            case JobsOperation.Get: @params["get"] = "1"; break;
            case JobsOperation.GetAll: @params["get_all"] = "1"; break;
            default: throw new NotImplementedException($"Unknown jobs operation {c.Operation}");
        }

        if (!string.IsNullOrEmpty(c.Id)) @params["id"] = c.Id;
        if (!string.IsNullOrEmpty(c.Name)) @params["name"] = c.Name;
        if (!string.IsNullOrEmpty(c.Phones)) @params["phones"] = c.Phones;
        if (!string.IsNullOrEmpty(c.Message)) @params["mes"] = c.Message;
        if (!string.IsNullOrEmpty(c.Message2)) @params["mes2"] = c.Message2;
        if (!string.IsNullOrEmpty(c.Sender)) @params["sender"] = c.Sender;
        if (!string.IsNullOrEmpty(c.Sender2)) @params["sender2"] = c.Sender2;
        if (!string.IsNullOrEmpty(c.Subject)) @params["subj"] = c.Subject;
        if (!string.IsNullOrEmpty(c.Time)) @params["time"] = c.Time;
        if (!string.IsNullOrEmpty(c.Start)) @params["start"] = c.Start;
        if (!string.IsNullOrEmpty(c.End)) @params["end"] = c.End;
        if (!string.IsNullOrEmpty(c.List)) @params["list"] = c.List;
        if (!string.IsNullOrEmpty(c.BotName)) @params["bot"] = c.BotName;
        if (!string.IsNullOrEmpty(c.FileUrl)) @params["fileurl"] = c.FileUrl;
        if (!string.IsNullOrEmpty(c.TemplateId)) @params["tpl"] = c.TemplateId;
        if (c.Repeat != JobRepeat.None) @params["rpt"] = ((int)c.Repeat).ToString();
        if (c.RepeatCount is not null) @params["rptn"] = c.RepeatCount.Value.ToString();
        if (c.FallbackToSms) @params["fl[5]"] = "1";

        AddSmsTypeFlag(@params, c.SmsType);
        AddResponseFormat(@params);
        AddCharset(@params);
        return @params;
    }

    private static void AddSmsTypeFlag(Dictionary<string, string> @params, SmsType type)
    {
        switch (type)
        {
            case SmsType.Default: return;
            case SmsType.Flash: @params["flash"] = "1"; break;
            case SmsType.WapPush: @params["push"] = "1"; break;
            case SmsType.HLR: @params["hlr"] = "1"; break;
            case SmsType.Ping: @params["ping"] = "1"; break;
            case SmsType.MMS: @params["mms"] = "1"; break;
            case SmsType.Mail: @params["mail"] = "1"; break;
            case SmsType.Call: @params["call"] = "1"; break;
            case SmsType.Viber: @params["viber"] = "1"; break;
            case SmsType.Socials: @params["soc"] = "1"; break;
            case SmsType.Telegram: @params["tg"] = "1"; break;
            case SmsType.WhatsApp: break;
            case SmsType.Bin: @params["bin"] = "1"; break;
        }
    }
}
