namespace SMSC.Exceptions.StatusErrors;

public class MessageLimitExceededError : ISmsError
{
    public int Code => 247;

    public string Name => "Превышен лимит сообщений";

    public string Description => """
        Превышен общий суточный лимит сообщений, указанный Клиентом в личном кабинете в пункте "Настройки".
        """;
}
