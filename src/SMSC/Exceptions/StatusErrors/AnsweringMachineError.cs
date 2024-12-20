namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Зафиксирован автоответчик</i>
/// </summary>
public class AnsweringMachineError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 242;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Зафиксирован автоответчик";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Означает, что во время отправки голосового сообщения на стороне абонента был зафиксирован автоответчик.";
}
