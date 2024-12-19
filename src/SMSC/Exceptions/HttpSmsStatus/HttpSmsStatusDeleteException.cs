namespace SMSC.Exceptions.HttpSmsStatus;

/// <summary>
/// Ошибка удаления сообщения
/// </summary>
public class HttpSmsStatusDeleteException : SmsExceptionAbstract
{
    /// <summary>
    /// Номер ошибки в соответствии с <seealso href="https://smsc.ru/api/http/status_messages/status_answer/#menu">документацией</seealso>
    /// </summary>
    public override int Id { get; } = 5;
    /// <summary>
    /// Ошибка удаления сообщения.
    /// </summary>
    public override string Message => "Failed to delete message.";
}
