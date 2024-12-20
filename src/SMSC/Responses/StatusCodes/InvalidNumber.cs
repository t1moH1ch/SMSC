namespace SMSC.Responses.StatusCodes;

/// <summary>
/// Статус для <i>Неверный номер</i>
/// </summary>
public class InvalidNumber : IStatusCode
{
    /// <summary>
    /// Код статуса
    /// </summary>
    public int Code => 22;
    /// <summary>
    /// Название статуса
    /// </summary>
    public string Name => "Неверный номер";
    /// <summary>
    /// Описание статуса
    /// </summary>
    public string Description => "Неправильный формат номера телефона.";
}
