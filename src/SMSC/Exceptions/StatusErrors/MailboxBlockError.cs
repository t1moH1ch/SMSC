namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Почтовый ящик заблокирован</i>
/// </summary>
public class MailboxBlockError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 4;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Почтовый ящик заблокирован";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "От почтового сервера получателя вернулась ошибка о невозможности доставки сообщения по причине " +
        "блокировки ящика.";
}
