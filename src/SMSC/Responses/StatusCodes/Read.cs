namespace SMSC.Responses.StatusCodes;

public class Read : IStatusCode
{
    public int Code => 2;

    public string Name => "Прочитано";

    public string Description => "Сообщение было прочитано (открыто) абонентом. Данный статус возможен для e-mail-сообщений, имеющих формат html-документа.";
}
