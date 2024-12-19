namespace SMSC.Exceptions.StatusErrors;

public class ServiceNotConnectedError : ISmsError
{
    public int Code => 11;
    public string Name => "Не подключена услуга";
    public string Description => "Означает, что абонент не может принять SMS-сообщение. " +
        "Например, услуга не подключена, или абонент находится в роуминге, ." +
        "где не активирован прием сообщений, или у оператора абонента не налажен обмен SMS с текущим роуминговым оператором. " +
        "Также это может быть городской номер без приема сообщений.";
}
