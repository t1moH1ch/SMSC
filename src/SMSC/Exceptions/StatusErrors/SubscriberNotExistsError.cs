namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Абонент не существует</i>
/// </summary>
public class SubscriberNotExistsError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 1;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Абонент не существует";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Указанный номер телефона не существует.";
}
