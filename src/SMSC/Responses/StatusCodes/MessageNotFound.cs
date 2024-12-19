namespace SMSC.Responses.StatusCodes;

public class MessageNotFound : IStatusCode
{
    public int Code => -3;

    public string Name => "Сообщение не найдено";

    public string Description => """
        Возникает, если для указанного номера телефона и ID сообщение не найдено.
        """;
}
