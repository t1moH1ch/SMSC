namespace SMSC.Responses.StatusCodes;

/// <summary>
/// Статус для <i>Доставлено</i>
/// </summary>
public class Delivered : IStatusCode
{
    /// <summary>
    /// Код статуса
    /// </summary>
    public int Code => 1;
    /// <summary>
    /// Название статуса
    /// </summary>
    public string Name => "Доставлено";
    /// <summary>
    /// Описание статуса
    /// </summary>
    public string Description => "Сообщение было успешно доставлено абоненту.";
}
