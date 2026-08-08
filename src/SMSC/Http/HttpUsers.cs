namespace SMSC.Http;

/// <summary>
/// Управление субклиентами (<seealso href="https://smsc.ru/api/http/users/">users.php</seealso>).
/// </summary>
public class HttpUsers(
    ProviderConfiguration configuration,
    HttpClient? httpClient = null) : HttpAbstract(configuration, httpClient)
{
    private UsersConfiguration? _config => SmsConfiguration as UsersConfiguration;

    protected override string SiteAddressCommand { get; } = "users.php";

    public virtual Task<IEnumerable<HttpUsersResponse>> AddAsync(string user, string password, UsersConfiguration? extra = null, CancellationToken ct = default)
    {
        extra ??= new UsersConfiguration();
        extra.Operation = UsersOperation.Add;
        extra.User = user;
        extra.Password = password;
        return ExecuteAsync(extra, ct);
    }

    public virtual Task<IEnumerable<HttpUsersResponse>> ChangeAsync(string user, UsersConfiguration? extra = null, CancellationToken ct = default)
    {
        extra ??= new UsersConfiguration();
        extra.Operation = UsersOperation.Change;
        extra.User = user;
        return ExecuteAsync(extra, ct);
    }

    public virtual Task<IEnumerable<HttpUsersResponse>> PayAsync(string user, string sum, CancellationToken ct = default)
        => ExecuteAsync(new UsersConfiguration { Operation = UsersOperation.Pay, User = user, Sum = sum }, ct);

    public virtual Task<IEnumerable<HttpUsersResponse>> GetStatAsync(UsersConfiguration? config = null, CancellationToken ct = default)
    {
        config ??= new UsersConfiguration();
        config.Operation = UsersOperation.GetStat;
        return ExecuteAsync(config, ct);
    }

    public virtual Task<IEnumerable<HttpUsersResponse>> GetStatAllAsync(string? start = null, string? end = null, CancellationToken ct = default)
        => ExecuteAsync(new UsersConfiguration { Operation = UsersOperation.GetStatAll, Start = start, End = end, Reseller = true }, ct);

    public virtual async Task<IEnumerable<HttpUsersResponse>> ExecuteAsync(UsersConfiguration config, CancellationToken cancellationToken = default)
    {
        SmsConfiguration = config;
        return await (await SendRequest(CreateRequest(), cancellationToken))
            .CreateHttpUsersResponseAsync(config);
    }

    protected override Dictionary<string, string> GetQueryDictionary()
    {
        CheckSmsConfiguration();
        var c = _config!;
        var @params = new Dictionary<string, string>();

        switch (c.Operation)
        {
            case UsersOperation.Add: @params["add"] = "1"; break;
            case UsersOperation.Change: @params["chg"] = "1"; break;
            case UsersOperation.Pay: @params["pay"] = "1"; break;
            case UsersOperation.GetStat: @params["get_stat"] = "1"; break;
            case UsersOperation.GetStatAll: @params["get_stat_all"] = "1"; break;
            default: throw new NotImplementedException($"Unknown users operation {c.Operation}");
        }

        if (!string.IsNullOrEmpty(c.User)) @params["user"] = c.User;
        if (!string.IsNullOrEmpty(c.UserId)) @params["user_id"] = c.UserId;
        if (!string.IsNullOrEmpty(c.Password)) @params["password"] = c.Password;
        if (!string.IsNullOrEmpty(c.NewLogin)) @params["new_login"] = c.NewLogin;
        if (!string.IsNullOrEmpty(c.Fio)) @params["fio"] = c.Fio;
        if (!string.IsNullOrEmpty(c.Phone)) @params["phone"] = c.Phone;
        if (!string.IsNullOrEmpty(c.Email)) @params["email"] = c.Email;
        if (!string.IsNullOrEmpty(c.Sum)) @params["sum"] = c.Sum;
        if (!string.IsNullOrEmpty(c.Start)) @params["start"] = c.Start;
        if (!string.IsNullOrEmpty(c.End)) @params["end"] = c.End;
        if (!string.IsNullOrEmpty(c.Sender)) @params["sender"] = c.Sender;
        if (c.Type is not null) @params["type"] = c.Type.Value.ToString();
        if (c.Reseller) @params["reseller"] = "1";
        if (c.Balance2) @params["balance2"] = "1";
        if (c.OwnCurrency) @params["mycur"] = "1";
        if (c.AllowOwnSender) @params["fl[29]"] = "1";

        AddResponseFormat(@params);
        AddCharset(@params);
        return @params;
    }
}
