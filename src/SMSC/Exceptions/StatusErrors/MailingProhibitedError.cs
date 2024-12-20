namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Рассылка запрещена</i>
/// </summary>
public class MailingProhibitedError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 244;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Рассылка запрещена";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Означает, что для данного Клиента запрещена отправка массовых и рекламных рассылок, " +
        "либо в тексте сообщения встретилась запрещенная ссылка.";
}
