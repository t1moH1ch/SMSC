namespace SMSC.Exceptions.HttpSms;

/// <summary>
/// Ошибка авторизации в сервисе
/// </summary>
public class HttpSmsWrongCredentialsException : SmsExceptionAbstract
{
    /// <summary>
    /// Номер ошибки в соответствии с <seealso href="https://smsc.ru/api/#menu">документацией</seealso>
    /// </summary>
    public override int Id { get; } = 2;
    /// <summary>
    /// Неверный логин или пароль. Также возникает при попытке отправки сообщения с IP-адреса, 
    /// не входящего в <see href="https://www.smsc.ru/ips/">список</see> разрешенных Клиентом (если такой список был настроен Клиентом ранее).
    /// </summary>
    public override string Message => $"Invalid username or password.";
}
