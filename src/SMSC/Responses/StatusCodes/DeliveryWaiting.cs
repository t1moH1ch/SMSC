namespace SMSC.Responses.StatusCodes;

public class DeliveryWaiting : IStatusCode
{
    public int Code => -1;

    public string Name => "Ожидает отправки";

    public string Description => """
        Если при отправке сообщения было задано время получения абонентом, то до этого времени сообщение будет находиться в данном статусе, 
        в других случаях сообщение в этом статусе находится непродолжительное время перед отправкой на SMS-центр.
        """;
}
