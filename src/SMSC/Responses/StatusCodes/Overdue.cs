namespace SMSC.Responses.StatusCodes;

public class Overdue : IStatusCode
{
    public int Code => 3;

    public string Name => "Просрочено";

    public string Description => """
        Возникает, если время "жизни" сообщения истекло, а оно так и не было доставлено получателю, например, если 
        абонент не был доступен в течение определенного времени или в его телефоне был переполнен буфер сообщений.
        """;
}
