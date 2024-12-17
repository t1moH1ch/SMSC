namespace SMSC.Http;

/// <summary>
/// Обработка запросов для получения статуса отправленного СМС
/// </summary>
/// <param name="configuration"></param>
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
    /// <param name="client">Номер мобильного телефона в международном формате, на который отправляется сообщение. 
    /// Номер могут передаваться без знака "+". Если номер передан без знака "+", то он может быть исправлен автоматическим 
    /// форматированием и приведен к правильному международному формату. Таким образом, некоторые ошибки при вводе номеров 
    /// телефонов могут быть исправлены автоматически. Для отключения автоисправления передайте номер со знаком "+".<br/>
    /// Для <b>e-mail</b> сообщения передается список e-mail адресов получателей.<br/>
    /// </param>
    /// <param name="config">Конфигурация отправляемого сообщения</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <exception cref="MessageMaxLengthException"/>
    public virtual async Task<IEnumerable<HttpSmsStatusResponse>> CheckSms(string client, SmsStatusConfiguration config, CancellationToken cancellationToken = default)
    {
        SmsConfiguration = config;

        return await (await SendRequest(CreateRequest((@params) =>
        {
            if (!string.IsNullOrEmpty(client))
                @params.Add("phone", client);
        }), cancellationToken))
            .CreateHttpSmsStatusResponseAsync(config);
    }

    /// <summary>
    /// Настройка строки запроса
    /// </summary>
    /// <returns>Список параметров в виде словаря (ключ-значение)</returns>
    /// <exception cref="ArgumentException"/>
    protected override Dictionary<string, string> GetQueryDictionary()
    {
        CheckSmsConfiguration();

        var @params = new Dictionary<string, string>();

        if (_smsConfiguration!.Id is not null)
            @params.Add("id", _smsConfiguration.Id);

        AddResponseFormat(@params);

        AddStatusTypeParameter(@params);
        AddCharset(@params);

        if (_smsConfiguration!.Delete)
        {
            ArgumentException.ThrowIfNullOrEmpty(_smsConfiguration!.Id);
            @params.Add("del", "1");
        }

        return @params;
    }
    /// <summary>
    /// Добавление в список параметров информации о возвращаемом статусе сообщения
    /// </summary>
    /// <param name="params">Список параметров сообщения</param>
    /// <exception cref="ArgumentException"></exception>
    private void AddStatusTypeParameter(Dictionary<string, string> @params)
    {
        if (_smsConfiguration!.StatusType != StatusType.Default)
        {
            switch (_smsConfiguration!.StatusType)
            {
                case StatusType.Full: @params.Add("all", "1"); break;
                case StatusType.Additional: @params.Add("all", "2"); break;
                default: throw new ArgumentException($"Unknown status type - {Enum.GetName(_smsConfiguration.StatusType)}");
            }
        }
    }
}
