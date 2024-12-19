namespace SMSC.Responses.StatusCodes;

public class InvalidNumber : IStatusCode
{
    public int Code => 22;

    public string Name => "Неверный номер";

    public string Description => "Неправильный формат номера телефона.";
}
