namespace SMSC.Responses.StatusCodes;

/// <summary>
/// Статус для <i>Прочитано</i>
/// </summary>
public class Read : IStatusCode
{
    /// <summary>
    /// Код статуса
    /// </summary>
    public int Code => 2;
    /// <summary>
    /// Название статуса
    /// </summary>
    public string Name => "Прочитано";
    /// <summary>
    /// Описание статуса
    /// </summary>
    public string Description => "Сообщение было прочитано (открыто) абонентом. Данный статус возможен для e-mail-сообщений, " +
        "имеющих формат html-документа.";
}
