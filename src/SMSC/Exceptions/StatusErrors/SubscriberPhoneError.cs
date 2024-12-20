namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Ошибка в телефоне абонента</i>
/// </summary>
public class SubscriberPhoneError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 12;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Ошибка в телефоне абонента";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Не удается доставить сообщение абоненту из-за ошибки в телефонном аппарате или SIM-карте.";
}
