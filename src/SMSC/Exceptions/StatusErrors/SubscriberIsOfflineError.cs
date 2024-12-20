namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Абонент не в сети</i>
/// </summary>
public class SubscriberIsOfflineError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 6;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Абонент не в сети";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Телефон абонента отключен или находится вне зоны действия сети.";
}
