namespace SMSC.Exceptions.StatusErrors;

public class UnregisteredSenderIdError : ISmsError
{
    public int Code => 254;

    public string Name => "Незарегистрированный sender id";

    public string Description => """
        Данная ошибка возникает при попытке отправки сообщения от незарегистрированного имени отправителя.
        """;
}
