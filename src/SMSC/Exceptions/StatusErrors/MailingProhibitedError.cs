namespace SMSC.Exceptions.StatusErrors;

public class MailingProhibitedError : ISmsError
{
    public int Code => 244;

    public string Name => "Рассылка запрещена";

    public string Description => """
        Означает, что для данного Клиента запрещена отправка массовых и рекламных рассылок, 
        либо в тексте сообщения встретилась запрещенная ссылка.
        """;
}
