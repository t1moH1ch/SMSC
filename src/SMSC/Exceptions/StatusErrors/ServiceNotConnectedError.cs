namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Не подключена услуга</i>
/// </summary>
public class ServiceNotConnectedError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 11;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Не подключена услуга";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Означает, что абонент не может принять SMS-сообщение. " +
        "Например, услуга не подключена, или абонент находится в роуминге, " +
        "где не активирован прием сообщений, или у оператора абонента не налажен обмен SMS с текущим роуминговым оператором. " +
        "Также это может быть городской номер без приема сообщений.";
}