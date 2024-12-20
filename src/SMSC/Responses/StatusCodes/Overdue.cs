namespace SMSC.Responses.StatusCodes;

/// <summary>
/// Статус для <i>Просрочено</i>
/// </summary>
public class Overdue : IStatusCode
{
    /// <summary>
    /// Код статуса
    /// </summary>
    public int Code => 3;
    /// <summary>
    /// Название статуса
    /// </summary>
    public string Name => "Просрочено";
    /// <summary>
    /// Описание статуса
    /// </summary>
    public string Description => "Возникает, если время \"жизни\" сообщения истекло, а оно так и не было доставлено получателю, например, если"
        + "абонент не был доступен в течение определенного времени или в его телефоне был переполнен буфер сообщений.";
}
