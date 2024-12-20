namespace SMSC.Responses.StatusCodes;

/// <summary>
/// Статус для <i>Передано оператору</i>
/// </summary>
public class OperatorPassed : IStatusCode
{
    /// <summary>
    /// Код статуса
    /// </summary>
    public int Code => 0;
    /// <summary>
    /// Название статуса
    /// </summary>
    public string Name => "Передано оператору";
    /// <summary>
    /// Описание статуса
    /// </summary>
    public string Description => "Сообщение было передано на SMS-центр оператора для доставки.";
}
