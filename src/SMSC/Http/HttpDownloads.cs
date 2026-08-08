namespace SMSC.Http;

/// <summary>
/// Действия с отложенными задачами выгрузки (<seealso href="https://smsc.ru/api/http/miscellaneous/downloads/">downloads.php</seealso>).
/// </summary>
public class HttpDownloads(
    ProviderConfiguration configuration,
    HttpClient? httpClient = null) : HttpAbstract(configuration, httpClient)
{
    private DownloadsConfiguration? _config => SmsConfiguration as DownloadsConfiguration;

    protected override string SiteAddressCommand { get; } = "downloads.php";

    public virtual Task<IEnumerable<HttpDownloadResponse>> GetListAsync(int? count = null, string? afterId = null, CancellationToken ct = default)
        => ExecuteAsync(new DownloadsConfiguration { Operation = DownloadsOperation.GetList, Count = count, AfterId = afterId }, ct);

    public virtual Task<IEnumerable<HttpDownloadResponse>> GetFileAsync(string? id = null, string? name = null, CancellationToken ct = default)
        => ExecuteAsync(new DownloadsConfiguration { Operation = DownloadsOperation.GetFile, Id = id, Name = name }, ct);

    public virtual async Task<IEnumerable<HttpDownloadResponse>> ExecuteAsync(DownloadsConfiguration config, CancellationToken cancellationToken = default)
    {
        SmsConfiguration = config;
        return await (await SendRequest(CreateRequest(), cancellationToken))
            .CreateHttpDownloadResponseAsync(config);
    }

    protected override Dictionary<string, string> GetQueryDictionary()
    {
        CheckSmsConfiguration();
        var c = _config!;
        var @params = new Dictionary<string, string>();

        switch (c.Operation)
        {
            case DownloadsOperation.GetList: @params["get_list"] = "1"; break;
            case DownloadsOperation.GetFile: @params["get_file"] = "1"; break;
            default: throw new NotImplementedException($"Unknown downloads operation {c.Operation}");
        }

        if (!string.IsNullOrEmpty(c.Id)) @params["id"] = c.Id;
        if (!string.IsNullOrEmpty(c.Name)) @params["name"] = c.Name;
        if (c.Count is not null) @params["cnt"] = c.Count.Value.ToString();
        if (!string.IsNullOrEmpty(c.AfterId)) @params["after_id"] = c.AfterId;

        AddResponseFormat(@params);
        AddCharset(@params);
        return @params;
    }
}
