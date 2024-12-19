namespace SMSC.Exceptions.StatusErrors;

public class SubscriberNotRespondingError : ISmsError
{
    public int Code => 237;
    public string Name => "Абонент не отвечает";
    public string Description => "Возникает, если в процессе попытки дозвона абонент не взял трубку.";
}
