namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Превышен лимит сообщений</i>
/// </summary>
public class MessageLimitExceededError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 247;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Превышен лимит сообщений";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Превышен общий суточный лимит сообщений, указанный Клиентом в личном кабинете в пункте \"Настройки\".";
}
