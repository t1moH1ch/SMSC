namespace SMSC.Exceptions.StatusErrors;

public class SubscriberNotExistsError : ISmsError
{
    public int Code => 1;
    public string Name => "Абонент не существует";
    public string Description => "Указанный номер телефона не существует.";
}
