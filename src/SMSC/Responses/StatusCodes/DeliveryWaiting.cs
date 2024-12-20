namespace SMSC.Responses.StatusCodes;

/// <summary>
/// Статус для <i>Ожидает отправки</i>
/// </summary>
public class DeliveryWaiting : IStatusCode
{
    /// <summary>
    /// Код статуса
    /// </summary>
    public int Code => -1;
    /// <summary>
    /// Название статуса
    /// </summary>
    public string Name => "Ожидает отправки";
    /// <summary>
    /// Описание статуса
    /// </summary>
    public string Description => "Если при отправке сообщения было задано время получения абонентом, " +
        "то до этого времени сообщение будет находиться в данном статусе, в других случаях сообщение в этом статусе " +
        "находится непродолжительное время перед отправкой на SMS-центр.";
}
