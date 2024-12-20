namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Незарегистрированный sender id</i>
/// </summary>
public class UnregisteredSenderIdError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 254;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Незарегистрированный sender id";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Данная ошибка возникает при попытке отправки сообщения от незарегистрированного имени отправителя.";
}
