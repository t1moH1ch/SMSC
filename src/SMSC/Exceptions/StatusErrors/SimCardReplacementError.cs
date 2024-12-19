namespace SMSC.Exceptions.StatusErrors;

public class SimCardReplacementError : ISmsError
{
    public int Code => 219;
    public string Name => "Замена sim-карты";
    public string Description => "Ошибка отправки сообщения в связи с заменой абонентом sim-карты. " +
        "После физической замены sim-карты абоненту заблокирован прием сообщений от буквенных имен отправителей на 24 часа. " +
        "Блокировка может быть снята путем пополнения баланса на 100 рублей.";
}
