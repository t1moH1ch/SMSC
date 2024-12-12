namespace SMSC.Http;

public class HttpSmsBalance(
    ProviderConfiguration providerConfiguration) : HttpAbstract(providerConfiguration)
{
    private SmsBalanceConfiguration? _smsConfiguration => SmsConfiguration as SmsBalanceConfiguration;

    /// <summary>
    /// Название endpoint для отправки команд
    /// </summary>
    protected override string SiteAddressCommand { get; } = "balance.php";

    /// <summary>
    /// Проверка баланса
    /// </summary>
    /// <param name="config">Список параметров сообщения</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    /// <exception cref="MessageMaxLengthException"/>
    public virtual async Task<HttpSmsBalanceResponse> CheckBalance(SmsBalanceConfiguration config, CancellationToken cancellationToken = default)
    {
        SmsConfiguration = config;

        return await (await SendRequest(CreateRequest(), cancellationToken))
            .CreateHttpSmsBalanceResponseAsync(config);
    }

    /// <summary>
    /// Настройка строки запроса
    /// </summary>
    /// <param name="config">Конфигурация для отправки запроса</param>
    /// <returns>Список параметров в виде словаря (ключ-значение)</returns>
    /// <exception cref="ArgumentException"/>
    protected override Dictionary<string, string> GetQueryDictionary()
    {
        CheckSmsConfiguration();

        var @params = new Dictionary<string, string>();

        AddResponseFormat(@params);

        AddCharset(@params);

        if (_smsConfiguration!.Currency)
            @params.Add("cur", "1");

        return @params;
    }
}
