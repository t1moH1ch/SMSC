namespace SMSC.Exceptions.StatusErrors;

public class SubscriberBusyError : ISmsError
{
    public int Code => 240;
    public string Name => "Абонент занят";
    public string Description => "Возникает при передаче голосового сообщения абоненту, если линия занята или абонент отменил вызов.";
}
