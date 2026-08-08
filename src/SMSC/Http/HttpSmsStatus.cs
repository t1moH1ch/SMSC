namespace SMSC.Http;

public class HttpSmsStatus(
    ProviderConfiguration configuration) : HttpAbstract(configuration)
{
    private SmsStatusConfiguration? _smsConfiguration => SmsConfiguration as SmsStatusConfiguration;

    /// <summary>
    /// Название endpoint для отправки команд
    /// </summary>
    protected override string SiteAddressCommand { get; } = "status.php";

    /// <summary>
    /// Проверка статуса доставки SMS или e-mail
    /// </summary>
    public virtual async Task<IEnumerable<HttpSmsStatusResponse>> CheckSms(string client, SmsStatusConfiguration config, CancellationToken cancellationToken = default)
    {
        SmsConfiguration = config;

        return await (await SendRequest(CreateRequest((@params) =>
        {
            if (!config.Bot && !string.IsNullOrEmpty(client))
                @params.Add("phone", client);
        }), cancellationToken))
            .CreateHttpSmsStatusResponseAsync(config);
    }

    /// <summary>
    /// Пакетная проверка статусов по спискам идентификаторов и телефонов (через запятую в API).
    /// </summary>
    public virtual async Task<IEnumerable<HttpSmsStatusResponse>> CheckSms(
        IEnumerable<string> phones,
        IEnumerable<string> ids,
        SmsStatusConfiguration config,
        CancellationToken cancellationToken = default)
    {
        ThrowHelper.ThrowIfNull(phones);
        ThrowHelper.ThrowIfNull(ids);

        var phoneList = phones.Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
        var idList = ids.Where(i => !string.IsNullOrWhiteSpace(i)).ToArray();

        if (idList.Length == 0 && !config.Bot)
            throw new ArgumentException("At least one message id is required.", nameof(ids));

        config.Id = string.Join(",", idList);
        SmsConfiguration = config;

        return await (await SendRequest(CreateRequest((@params) =>
        {
            if (!config.Bot && phoneList.Length > 0)
                @params.Add("phone", string.Join(",", phoneList));
        }), cancellationToken))
            .CreateHttpSmsStatusResponseAsync(config);
    }

    /// <inheritdoc/>
    protected override Dictionary<string, string> GetQueryDictionary()
    {
        CheckSmsConfiguration();

        var @params = new Dictionary<string, string>();

        if (_smsConfiguration!.Bot)
        {
            @params.Add("bot", "1");
        }
        else if (_smsConfiguration.Id is not null)
        {
            @params.Add("id", _smsConfiguration.Id);
        }

        AddResponseFormat(@params);
        AddStatusTypeParameter(@params);
        AddCharset(@params);

        if (_smsConfiguration!.Delete)
        {
            ThrowHelper.ThrowIfNullOrEmpty(_smsConfiguration!.Id);
            @params.Add("del", "1");
        }

        return @params;
    }

    private void AddStatusTypeParameter(Dictionary<string, string> @params)
    {
        if (_smsConfiguration!.StatusType != StatusType.Default)
        {
            switch (_smsConfiguration!.StatusType)
            {
                case StatusType.Full: @params.Add("all", "1"); break;
                case StatusType.Additional: @params.Add("all", "2"); break;
                default: throw new ArgumentException($"Unknown status type - {_smsConfiguration.StatusType}");
            }
        }
    }
}
