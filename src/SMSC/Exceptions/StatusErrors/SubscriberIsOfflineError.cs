namespace SMSC.Exceptions.StatusErrors;

public class SubscriberIsOfflineError : ISmsError
{
    public int Code => 6;
    public string Name => "Абонент не в сети";
    public string Description => "Телефон абонента отключен или находится вне зоны действия сети.";
}
