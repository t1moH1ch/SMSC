namespace SMSC.Exceptions.StatusErrors;

public class OneNumberLimitExceededError : ISmsError
{
    public int Code => 251;

    public string Name => "Превышен лимит на один номер";

    public string Description => """
        Превышен суточный лимит сообщений на один номер. Лимит устанавливается Клиентом в личном кабинете в пункте "Настройки". 
        Также такая ошибка возможна при отправке более 50 сообщений одному абоненту, которые были отправлены с перерывом между 
        сообщениями менее 30 секунд.
        """;
}
