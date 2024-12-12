namespace SMSC.Exceptions;

public class MessageMaxLengthException(
    int maxLength) : Exception
{
    private const string HelpLinkSource = "https://www.smsc.ru/api/http/send/sms/#menu";

    public override string Message => $"Exceeding max length message length. Max length = {maxLength}.";
    public override string? HelpLink { get => HelpLinkSource; }
}
