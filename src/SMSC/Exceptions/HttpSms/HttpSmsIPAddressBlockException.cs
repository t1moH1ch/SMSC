namespace SMSC.Exceptions.HttpSms;

/// <summary>
/// Временная блокировка IP-адреса
/// </summary>
public class HttpSmsIPAddressBlockException : SmsExceptionAbstract
{
    /// <summary>
    /// Номер ошибки в соответствии с <seealso href="https://smsc.ru/api/#menu">документацией</seealso>
    /// </summary>
    public override int Id { get; } = 4;
    /// <summary>
    /// IP-адрес временно заблокирован из-за частых ошибок в запросах. <see href="https://www.smsc.ru/faq/99/">Подробнее</see>.
    /// </summary>
    public override string Message => "The IP address is temporarily blocked due to frequent errors in requests.";
}
