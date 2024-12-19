namespace SMSC.Responses.StatusCodes;

public class LinkClicked : IStatusCode
{
    public int Code => 4;

    public string Name => "Нажата ссылка";

    public string Description => """
        Сообщение было доставлено, и абонентом была нажата короткая ссылка, переданная в сообщении. 
        Данный статус возможен при включенных в настройках опциях "Автоматически сокращать ссылки в сообщениях" и 
        "отслеживать номера абонентов".
        """;
}
