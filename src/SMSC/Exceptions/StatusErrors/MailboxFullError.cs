namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Переполнен почтовый ящик</i>
/// </summary>
public class MailboxFullError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 2;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Переполнен почтовый ящик";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Почтовый сервер получателя отклонил сообщение с указанным кодом ошибки.";
}
