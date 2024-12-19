namespace SMSC.Responses.StatusCodes;

public class Delivered : IStatusCode
{
    public int Code => 1;

    public string Name => "Доставлено";

    public string Description => "Сообщение было успешно доставлено абоненту.";
}
