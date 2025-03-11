namespace SMSC.Exceptions.HttpSms;

/// <summary>
/// Ошибка в параметрах отправляемого сообщения
/// </summary>
/// <param name="message"></param>
public class HttpSmsParametersException(
    string? message = null) : SmsExceptionAbstract
{
    /// <summary>
    /// Номер ошибки в соответствии с <seealso href="https://smsc.ru/api/#menu">документацией</seealso>
    /// </summary>
    public override int Id { get; } = 1;
    /// <summary>
    /// Ошибка в параметрах.
    /// </summary>
    public override string Message => string.IsNullOrEmpty(message) ? $"Error in the parameters" : message!;
}
