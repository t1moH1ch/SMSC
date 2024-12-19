namespace SMSC.Exceptions.StatusErrors;

public class NoTemplateError : ISmsError
{
    public int Code => 238;
    public string Name => "Нет шаблона";
    public string Description => """
        Возникает, если отправка сообщения возможна только по определенному шаблону, 
        а отправляемое сообщение ему не удовлетворяет.
        """;
}
