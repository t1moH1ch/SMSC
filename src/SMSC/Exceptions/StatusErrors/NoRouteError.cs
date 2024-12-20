namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Нет маршрута</i>
/// </summary>
public class NoRouteError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 248;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Нет маршрута";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Означает, что на данный номер отправка сообщений недоступна в нашем сервисе. " +
        "Например, ввели несуществующий мобильный код, либо для указанного номера и текста нет рабочего SMS-шлюза.";
}
