namespace SMSC.Exceptions.StatusErrors;

public class AnsweringMachineError : ISmsError
{
    public int Code => 242;
    public string Name => "Зафиксирован автоответчик";
    public string Description => """
        Означает, что во время отправки голосового сообщения на стороне абонента был зафиксирован автоответчик.
        """;
}
