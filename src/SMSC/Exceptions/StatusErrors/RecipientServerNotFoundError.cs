namespace SMSC.Exceptions.StatusErrors;

public class RecipientServerNotFoundError : ISmsError
{
    public int Code => 3;

    public string Name => "Сервер получателя не найден";

    public string Description => """
        Домен, указанный в качестве почтового сервера, не найден.
        """;
}
