namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Статус не получен</i>
/// </summary>
public class StatusNotReceivedError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 245;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Статус не получен";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "В течение суток статус доставки не был получен от оператора, в этом случае нельзя точно сказать, " +
        "было сообщение доставлено или нет.";
}
