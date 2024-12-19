namespace SMSC.Exceptions.StatusErrors;

public class MailboxBlockError : ISmsError
{
    public int Code => 4;

    public string Name => "Почтовый ящик заблокирован";

    public string Description => """
        От почтового сервера получателя вернулась ошибка о невозможности доставки сообщения по причине блокировки ящика.
        """;
}
