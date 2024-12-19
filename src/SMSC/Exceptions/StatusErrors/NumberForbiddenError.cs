namespace SMSC.Exceptions.StatusErrors;

public class NumberForbiddenError : ISmsError
{
    public int Code => 252;

    public string Name => "Номер запрещен";

    public string Description => """
        Возникает, например, при попытке указания Клиентом одного из наших федеральных номеров в качестве получателя SMS-сообщения.
        """;
}
