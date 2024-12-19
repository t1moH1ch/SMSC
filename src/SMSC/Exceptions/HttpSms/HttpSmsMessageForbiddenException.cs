namespace SMSC.Exceptions.HttpSms;

/// <summary>
/// Ошибка отправки сообщения (отсутствует имя отправителя или по тексту сообщения)
/// </summary>
public class HttpSmsMessageForbiddenException : SmsExceptionAbstract
{
    /// <summary>
    /// Номер ошибки в соответствии с <seealso href="https://smsc.ru/api/#menu">документацией</seealso>
    /// </summary>
    public override int Id { get; } = 6;
    /// <summary>
    /// Сообщение запрещено (по тексту или по имени отправителя). 
    /// Также данная ошибка возникает при попытке отправки массовых и (или) рекламных сообщений без заключенного договора.
    /// </summary>
    public override string Message => "The message is forbidden";
}
