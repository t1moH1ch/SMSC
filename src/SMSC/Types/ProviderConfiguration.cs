namespace SMSC.Types;

public class ProviderConfiguration
{
    private readonly Uri _smscApiAddress = new("https://smsc.ru/sys/");
    private readonly Uri _jsonSmsApiAddress = new("https://smsc.ru/rest/send/");

    public const string SmtpFrom = "api@smsc.ru";
    public const string SmtpServer = "send.smsc.ru";

    /// <summary>
    /// Статический адрес API сервиса <seealso href="https://www.smsc.ru/">SMSC.ru</seealso>
    /// </summary>
    public Uri SmscApiAddress { get => _smscApiAddress; }
    /// <summary>
    /// Статический адрес API сервиса <seealso href="https://www.smsc.ru/">SMSC.ru</seealso> для отправки SMS с 
    /// передачей параметров в формате json в теле запроса. 
    /// </summary>
    public Uri JsonSmscApiAddress { get => _jsonSmsApiAddress; }

    /// <summary>
    /// Логин Клиента
    /// </summary>
    public string? Login { get; }
    /// <summary>
    /// Пароль Клиента (можно добавить или изменить на <seealso href="https://smsc.ru/passwords/">данной</seealso> странице)
    /// </summary>
    public string? Password { get; }

    /// <summary>
    /// Специальный API-ключ, используемый для упрощенной авторизации вместо пары "логин+пароль" (можно создать на <seealso href="https://smsc.ru/passwords/">данной</seealso> странице)
    /// </summary>
    public string? ApiKey { get; }

    /// <summary>
    /// Логин для доступа email серверу SMSC.ru
    /// </summary>
    public string? SmtpLogin { get; set; }
    /// <summary>
    /// Пароль для доступа к email серверу SMSC.ru
    /// </summary>
    public string? SmtpPassword { get; set; }

    /// <summary>
    /// Инициализация провайдера посредством логина и пароля
    /// </summary>
    /// <param name="login"></param>
    /// <param name="password"></param>
	public ProviderConfiguration(string login, string password)
    {
        Login = login;
        Password = password;
    }
    /// <summary>
    /// Инициализация провайдера посредством ключа ApiKey, предварительно создав его <seealso href="https://www.smsc.ru/passwords/">здесь</seealso>.
    /// </summary>
    /// <param name="apiKey"></param>
    public ProviderConfiguration(string apiKey)
    {
        ApiKey = apiKey;
    }
}
