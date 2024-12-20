namespace SMSC.Responses.StatusCodes;

/// <summary>
/// Статус для <i>Запрещено</i>
/// </summary>
public class Forbidden : IStatusCode
{
    /// <summary>
    /// Код статуса
    /// </summary>
    public int Code => 23;
    /// <summary>
    /// Название статуса
    /// </summary>
    public string Name => "Запрещено";
    /// <summary>
    /// Описание статуса
    /// </summary>
    public string Description => "Возникает при срабатывании ограничений на отправку дублей, на частые сообщения на один номер (флуд), " +
        "на номера из черного списка, на запрещенные спам фильтром тексты или имена отправителей (Sender ID).";
}
