namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Виртуальная отправка</i>
/// </summary>
public class VirtualSendingError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 200;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Виртуальная отправка";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Данное уведомление появляется под статусом сообщения в случае отправки сообщения " +
        "в режиме тестирования (при установленной в настройках галочке \"Режим тестирования (виртуальная отправка без оплаты)\").";
}
