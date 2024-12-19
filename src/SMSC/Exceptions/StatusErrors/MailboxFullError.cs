namespace SMSC.Exceptions.StatusErrors;

public class MailboxFullError : ISmsError
{
    public int Code => 2;

    public string Name => "Переполнен почтовый ящик";

    public string Description => """
        Почтовый сервер получателя отклонил сообщение с указанным кодом ошибки.
        """;
}
