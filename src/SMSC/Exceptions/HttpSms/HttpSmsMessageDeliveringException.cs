namespace SMSC.Exceptions.HttpSms;

/// <summary>
/// Ошибка доставки сообщения
/// </summary>
public class HttpSmsMessageDeliveringException : SmsExceptionAbstract
{
    /// <summary>
    /// Номер ошибки в соответствии с <seealso href="https://smsc.ru/api/#menu">документацией</seealso>
    /// </summary>
    public override int Id { get; } = 8;
    /// <summary>
    /// Сообщение на указанный номер не может быть доставлено.
    /// </summary>
    public override string Message => "The message cannot be delivered to the specified number.";
}
