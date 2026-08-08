namespace SMSC.Http;

/// <summary>
/// Управление контактами и группами (<seealso href="https://smsc.ru/api/http/contact/">phones.php</seealso>).
/// </summary>
public class HttpContacts(
    ProviderConfiguration configuration,
    HttpClient? httpClient = null) : HttpAbstract(configuration, httpClient)
{
    private ContactsConfiguration? _config => SmsConfiguration as ContactsConfiguration;

    protected override string SiteAddressCommand { get; } = "phones.php";

    public virtual Task<IEnumerable<HttpContactResponse>> AddAsync(string phone, string? name = null, string? groupId = null, CancellationToken ct = default)
        => ExecuteAsync(new ContactsConfiguration { Operation = ContactOperation.Add, Phone = phone, Name = name, GroupId = groupId }, ct);

    public virtual Task<IEnumerable<HttpContactResponse>> AddGroupAsync(string name, CancellationToken ct = default)
        => ExecuteAsync(new ContactsConfiguration { Operation = ContactOperation.AddGroup, Name = name }, ct);

    public virtual Task<IEnumerable<HttpContactResponse>> ChangeAsync(string phone, string? name = null, string? newPhone = null, CancellationToken ct = default)
        => ExecuteAsync(new ContactsConfiguration { Operation = ContactOperation.Change, Phone = phone, Name = name, NewPhone = newPhone }, ct);

    public virtual Task<IEnumerable<HttpContactResponse>> ChangeGroupAsync(string groupId, string name, CancellationToken ct = default)
        => ExecuteAsync(new ContactsConfiguration { Operation = ContactOperation.ChangeGroup, GroupId = groupId, Name = name }, ct);

    public virtual Task<IEnumerable<HttpContactResponse>> DeleteAsync(string phone, CancellationToken ct = default)
        => ExecuteAsync(new ContactsConfiguration { Operation = ContactOperation.Delete, Phone = phone }, ct);

    public virtual Task<IEnumerable<HttpContactResponse>> DeleteGroupAsync(string groupId, CancellationToken ct = default)
        => ExecuteAsync(new ContactsConfiguration { Operation = ContactOperation.DeleteGroup, GroupId = groupId }, ct);

    public virtual Task<IEnumerable<HttpContactResponse>> GetAsync(string? phone = null, string? groupId = null, CancellationToken ct = default)
        => ExecuteAsync(new ContactsConfiguration { Operation = ContactOperation.Get, Phone = phone, GroupId = groupId }, ct);

    public virtual Task<IEnumerable<HttpContactResponse>> GetGroupsAsync(CancellationToken ct = default)
        => ExecuteAsync(new ContactsConfiguration { Operation = ContactOperation.GetGroup }, ct);

    public virtual Task<IEnumerable<HttpContactResponse>> AddBlackAsync(string phone, int? type = null, CancellationToken ct = default)
        => ExecuteAsync(new ContactsConfiguration { Operation = ContactOperation.AddBlack, Phone = phone, BlackListType = type }, ct);

    public virtual Task<IEnumerable<HttpContactResponse>> DeleteBlackAsync(string phone, CancellationToken ct = default)
        => ExecuteAsync(new ContactsConfiguration { Operation = ContactOperation.DeleteBlack, Phone = phone }, ct);

    public virtual Task<IEnumerable<HttpContactResponse>> ExecuteAsync(ContactsConfiguration config, CancellationToken cancellationToken = default)
    {
        SmsConfiguration = config;
        return SendAndParseAsync(cancellationToken);
    }

    private async Task<IEnumerable<HttpContactResponse>> SendAndParseAsync(CancellationToken cancellationToken)
        => await (await SendRequest(CreateRequest(), cancellationToken))
            .CreateHttpContactResponseAsync(_config!);

    protected override Dictionary<string, string> GetQueryDictionary()
    {
        CheckSmsConfiguration();
        var c = _config!;
        var @params = new Dictionary<string, string>();

        switch (c.Operation)
        {
            case ContactOperation.Add:
                @params["add"] = "1";
                break;
            case ContactOperation.AddGroup:
                @params["add_group"] = "1";
                break;
            case ContactOperation.Change:
                @params["chg"] = "1";
                break;
            case ContactOperation.ChangeGroup:
                @params["chg_group"] = "1";
                break;
            case ContactOperation.Delete:
                @params["del"] = "1";
                break;
            case ContactOperation.DeleteGroup:
                @params["del_group"] = "1";
                break;
            case ContactOperation.Get:
                @params["get"] = "1";
                break;
            case ContactOperation.GetGroup:
                @params["get_group"] = "1";
                break;
            case ContactOperation.AddBlack:
                @params["add_black"] = "1";
                break;
            case ContactOperation.DeleteBlack:
                @params["del_black"] = "1";
                break;
            case ContactOperation.MoveToGroup:
                @params["move_group"] = "1";
                break;
            case ContactOperation.CopyToGroup:
                @params["move_group"] = "2";
                break;
            case ContactOperation.RemoveFromGroup:
                @params["move_group"] = "3";
                break;
            default:
                throw new NotImplementedException($"Unknown contact operation {c.Operation}");
        }

        if (!string.IsNullOrEmpty(c.Phone))
            @params["phone"] = c.Phone;
        if (!string.IsNullOrEmpty(c.NewPhone))
            @params["new_phone"] = c.NewPhone;
        if (!string.IsNullOrEmpty(c.Name))
            @params["name"] = c.Name;
        if (!string.IsNullOrEmpty(c.GroupId))
            @params["grp"] = c.GroupId;
        if (c.BlackListType is not null)
            @params["type"] = c.BlackListType.Value.ToString();

        AddResponseFormat(@params);
        AddCharset(@params);
        return @params;
    }
}
