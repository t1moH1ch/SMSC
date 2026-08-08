namespace SMSC.Http;

/// <summary>
/// Получение истории сообщений и статистики (<seealso href="https://smsc.ru/api/http/get_data/">get.php</seealso>).
/// </summary>
public class HttpHistory(
    ProviderConfiguration configuration) : HttpAbstract(configuration)
{
    private HistoryConfiguration? _config => SmsConfiguration as HistoryConfiguration;

    protected override string SiteAddressCommand { get; } = "get.php";

    public virtual Task<IEnumerable<HttpHistoryMessageResponse>> GetMessagesAsync(HistoryConfiguration config, CancellationToken ct = default)
    {
        config.Operation = HistoryOperation.GetMessages;
        return ExecuteAsync(config, ct);
    }

    public virtual Task<IEnumerable<HttpHistoryMessageResponse>> GetAnswersAsync(HistoryConfiguration? config = null, CancellationToken ct = default)
    {
        config ??= new HistoryConfiguration();
        config.Operation = HistoryOperation.GetAnswers;
        return ExecuteAsync(config, ct);
    }

    public virtual Task<IEnumerable<HttpHistoryMessageResponse>> GetStatAsync(string start, string end, HistoryConfiguration? config = null, CancellationToken ct = default)
    {
        config ??= new HistoryConfiguration();
        config.Operation = HistoryOperation.GetStat;
        config.Start = start;
        config.End = end;
        return ExecuteAsync(config, ct);
    }

    public virtual Task<IEnumerable<HttpHistoryMessageResponse>> GetPriceAsync(CancellationToken ct = default)
        => ExecuteAsync(new HistoryConfiguration { Operation = HistoryOperation.GetPrice }, ct);

    public virtual async Task<IEnumerable<HttpHistoryMessageResponse>> ExecuteAsync(HistoryConfiguration config, CancellationToken cancellationToken = default)
    {
        SmsConfiguration = config;
        return await (await SendRequest(CreateRequest(), cancellationToken))
            .CreateHttpHistoryResponseAsync(config);
    }

    protected override Dictionary<string, string> GetQueryDictionary()
    {
        CheckSmsConfiguration();
        var c = _config!;
        var @params = new Dictionary<string, string>();

        switch (c.Operation)
        {
            case HistoryOperation.GetMessages: @params["get_messages"] = "1"; break;
            case HistoryOperation.GetAnswers: @params["get_answers"] = "1"; break;
            case HistoryOperation.GetStat: @params["get_stat"] = "1"; break;
            case HistoryOperation.GetPrice: @params["get_price"] = "1"; break;
            default: throw new NotImplementedException($"Unknown history operation {c.Operation}");
        }

        if (!string.IsNullOrEmpty(c.Start)) @params["start"] = c.Start;
        if (!string.IsNullOrEmpty(c.End)) @params["end"] = c.End;
        if (!string.IsNullOrEmpty(c.Phone)) @params["phone"] = c.Phone;
        if (!string.IsNullOrEmpty(c.Email)) @params["email"] = c.Email;
        if (!string.IsNullOrEmpty(c.ToPhone)) @params["to_phone"] = c.ToPhone;
        if (c.Format is not null) @params["format"] = c.Format.Value.ToString();
        if (c.Count is not null) @params["cnt"] = c.Count.Value.ToString();
        if (!string.IsNullOrEmpty(c.PrevId)) @params["prev_id"] = c.PrevId;
        if (!string.IsNullOrEmpty(c.AfterId)) @params["after_id"] = c.AfterId;
        if (c.Hour is not null) @params["hour"] = c.Hour.Value.ToString();
        if (c.OwnCurrency) @params["mycur"] = "1";
        if (c.Balance2) @params["balance2"] = "1";

        AddResponseFormat(@params);
        AddCharset(@params);
        return @params;
    }
}
