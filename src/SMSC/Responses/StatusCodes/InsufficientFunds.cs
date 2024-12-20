namespace SMSC.Responses.StatusCodes;

/// <summary>
/// Статус для <i>Недостаточно средств</i>
/// </summary>
public class InsufficientFunds : IStatusCode
{
    /// <summary>
    /// Код статуса
    /// </summary>
    public int Code => 24;
    /// <summary>
    /// Название статуса
    /// </summary>
    public string Name => "Недостаточно средств";
    /// <summary>
    /// Описание статуса
    /// </summary>
    public string Description => "На счете Клиента недостаточная сумма для отправки сообщения.";
}
