namespace SMSC.Responses.StatusCodes;

public class OperatorPassed : IStatusCode
{
    public int Code => 0;

    public string Name => "Передано оператору";

    public string Description => """
        Сообщение было передано на SMS-центр оператора для доставки.
        """;
}
