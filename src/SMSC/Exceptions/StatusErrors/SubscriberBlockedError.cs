namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Абонент заблокирован</i>
/// </summary>
public class SubscriberBlockedError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 13;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Абонент заблокирован";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Возникает, например, если на счету абонента нулевой или отрицательный баланс, " +
        "и он находится в роуминге, или заблокирован оператором за продолжительную неуплату либо добровольно самим абонентом. " +
        "Также данная ошибка может возвращаться при повреждении SIM-карты либо неправильном вводе PIN и PUK-кодов SIM-карты.";
}
