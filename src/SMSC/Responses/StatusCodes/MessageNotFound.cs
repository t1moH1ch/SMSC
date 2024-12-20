namespace SMSC.Responses.StatusCodes;

/// <summary>
/// Статус для <i>Сообщение не найдено</i>
/// </summary>
public class MessageNotFound : IStatusCode
{
    /// <summary>
    /// Код статуса
    /// </summary>
    public int Code => -3;
    /// <summary>
    /// Название статуса
    /// </summary>
    public string Name => "Сообщение не найдено";
    /// <summary>
    /// Описание статуса
    /// </summary>
    public string Description => "Возникает, если для указанного номера телефона и ID сообщение не найдено.";
}
