namespace SMSC.Exceptions.StatusErrors;

public class NoRouteError : ISmsError
{
    public int Code => 248;

    public string Name => "Нет маршрута";

    public string Description => """
        Означает, что на данный номер отправка сообщений недоступна в нашем сервисе. Например, ввели несуществующий мобильный код, 
        либо для указанного номера и текста нет рабочего SMS-шлюза.
        """;
}
