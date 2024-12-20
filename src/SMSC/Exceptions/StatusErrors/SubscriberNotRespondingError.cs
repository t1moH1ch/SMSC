namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Абонент не отвечает</i>
/// </summary>
public class SubscriberNotRespondingError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 237;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Абонент не отвечает";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Возникает, если в процессе попытки дозвона абонент не взял трубку.";
}
