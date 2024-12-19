namespace SMSC.Exceptions.StatusErrors;

public class NoServiceSupportError : ISmsError
{
    public int Code => 21;
    public string Name => "Нет поддержки сервиса";
    public string Description => "Аппарат абонента не поддерживает работу с данной услугой (сервисом).";
}
