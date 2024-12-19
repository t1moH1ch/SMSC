namespace SMSC.Exceptions.StatusErrors;

public class ForbiddenIpAddressError : ISmsError
{
    public int Code => 239;
    public string Name => "Запрещенный ip-адрес";
    public string Description => """
        Возникает при попытке отправки сообщения с ip-адреса, не входящего в список ip-адресов, 
        разрешенных Клиентом для отправки. Также может возникать при попытке отправки сообщения с ip-адреса, 
        ранее не используемого для отправки сообщений и входов в личный кабинет.
        """;
}
