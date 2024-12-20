namespace SMSC.Responses.StatusCodes;

/// <summary>
/// Статус для <i>Недоступный номер</i>
/// </summary>
public class UnavailableNumber : IStatusCode
{
    /// <summary>
    /// Код статуса
    /// </summary>
    public int Code => 25;
    /// <summary>
    /// Название статуса
    /// </summary>
    public string Name => "Недоступный номер";
    /// <summary>
    /// Описание статуса
    /// </summary>
    public string Description => "Телефонный номер не принимает SMS-сообщения, или на этого оператора нет рабочего маршрута.";
}
