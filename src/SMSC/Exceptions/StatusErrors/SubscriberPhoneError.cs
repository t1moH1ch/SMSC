namespace SMSC.Exceptions.StatusErrors;

public class SubscriberPhoneError : ISmsError
{
    public int Code => 12;
    public string Name => "Ошибка в телефоне абонента";
    public string Description => "Не удается доставить сообщение абоненту из-за ошибки в телефонном аппарате или SIM-карте.";
}
