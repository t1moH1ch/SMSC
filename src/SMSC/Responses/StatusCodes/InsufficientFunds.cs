namespace SMSC.Responses.StatusCodes;

public class InsufficientFunds : IStatusCode
{
    public int Code => 24;

    public string Name => "Недостаточно средств";

    public string Description => "На счете Клиента недостаточная сумма для отправки сообщения.";
}
