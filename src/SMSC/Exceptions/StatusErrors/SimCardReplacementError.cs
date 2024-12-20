namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Замена sim-карты</i>
/// </summary>
public class SimCardReplacementError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 219;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Замена sim-карты";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Ошибка отправки сообщения в связи с заменой абонентом sim-карты. " +
        "После физической замены sim-карты абоненту заблокирован прием сообщений от буквенных имен отправителей на 24 часа. " +
        "Блокировка может быть снята путем пополнения баланса на 100 рублей.";
}
