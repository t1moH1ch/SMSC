namespace SMSC.Exceptions.StatusErrors;

public class VirtualSendingError : ISmsError
{
    public int Code => 200;
    public string Name => "Виртуальная отправка";
    public string Description => "Данное уведомление появляется под статусом сообщения в случае отправки сообщения " +
        "в режиме тестирования (при установленной в настройках галочке \"Режим тестирования (виртуальная отправка без оплаты)\").";
}
