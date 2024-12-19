namespace SMSC.Responses.StatusCodes;

public class ImpossibleDeliver : IStatusCode
{
    public int Code => 20;

    public string Name => "Невозможно доставить";

    public string Description => """
        Попытка доставить сообщение закончилась неудачно, это может быть вызвано разными причинами, например, 
        абонент заблокирован, не существует, находится в роуминге без поддержки обмена SMS, или на его телефоне не 
        поддерживается прием SMS-сообщений.
        """;
}
