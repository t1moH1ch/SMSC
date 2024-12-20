namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Превышен лимит на один номер</i>
/// </summary>
public class OneNumberLimitExceededError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 251;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Превышен лимит на один номер";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Превышен суточный лимит сообщений на один номер. Лимит устанавливается Клиентом в личном кабинете в пункте \"Настройки\". " +
        "Также такая ошибка возможна при отправке более 50 сообщений одному абоненту, которые были отправлены с перерывом между " +
        "сообщениями менее 30 секунд.";
}
