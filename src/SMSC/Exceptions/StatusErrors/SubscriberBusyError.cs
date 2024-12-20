namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Абонент занят</i>
/// </summary>
public class SubscriberBusyError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 240;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Абонент занят";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Возникает при передаче голосового сообщения абоненту, если линия занята или абонент отменил вызов.";
}
