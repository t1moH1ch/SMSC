namespace SMSC.Responses.StatusCodes;

/// <summary>
/// Статус для <i>Невозможно доставить</i>
/// </summary>
public class ImpossibleDeliver : IStatusCode
{
    /// <summary>
    /// Код статуса
    /// </summary>
    public int Code => 20;
    /// <summary>
    /// Название статуса
    /// </summary>
    public string Name => "Невозможно доставить";
    /// <summary>
    /// Описание статуса
    /// </summary>
    public string Description => "Попытка доставить сообщение закончилась неудачно, это может быть вызвано разными причинами, например, " +
        "абонент заблокирован, не существует, находится в роуминге без поддержки обмена SMS, или на его телефоне не " +
        "поддерживается прием SMS-сообщений.";
}
