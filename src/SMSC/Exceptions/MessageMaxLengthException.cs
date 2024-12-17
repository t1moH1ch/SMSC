namespace SMSC.Exceptions;

/// <summary>
/// Превышение максимальной длины отправляемого сообщения
/// </summary>
/// <param name="maxLength"></param>
public class MessageMaxLengthException(
    int maxLength) : Exception
{
    private const string HelpLinkSource = "https://www.smsc.ru/api/http/send/sms/#menu";

    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public override string Message => $"Exceeding max length message length. Max length = {maxLength}.";
    /// <summary>
    /// Страничка справки об ошибке
    /// </summary>
    public override string? HelpLink { get => HelpLinkSource; }
}
