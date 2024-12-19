namespace SMSC.Exceptions.StatusErrors;

public class SubscriberBlockedError : ISmsError
{
    public int Code => 13;
    public string Name => "Абонент заблокирован";
    public string Description => "Возникает, например, если на счету абонента нулевой или отрицательный баланс, " +
        "и он находится в роуминге, или заблокирован оператором за продолжительную неуплату либо добровольно самим абонентом. " +
        "Также данная ошибка может возвращаться при повреждении SIM-карты либо неправильном вводе PIN и PUK-кодов SIM-карты.";
}
