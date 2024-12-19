namespace SMSC.Exceptions.StatusErrors;

public class NoError : ISmsError
{
    public int Code => 0;

    public string Name => "Нет ошибки";

    public string Description => "Получатель существует и доступен.";
}
