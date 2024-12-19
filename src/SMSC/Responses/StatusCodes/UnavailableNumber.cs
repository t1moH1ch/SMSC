namespace SMSC.Responses.StatusCodes;

public class UnavailableNumber : IStatusCode
{
    public int Code => 25;

    public string Name => "Недоступный номер";

    public string Description => "Телефонный номер не принимает SMS-сообщения, или на этого оператора нет рабочего маршрута.";
}
