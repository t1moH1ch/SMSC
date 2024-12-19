namespace SMSC.Exceptions.StatusErrors;

public class StatusNotReceivedError : ISmsError
{
    public int Code => 245;

    public string Name => "Статус не получен";

    public string Description => """
        В течение суток статус доставки не был получен от оператора, в этом случае нельзя точно сказать, было сообщение доставлено или нет.
        """;
}
