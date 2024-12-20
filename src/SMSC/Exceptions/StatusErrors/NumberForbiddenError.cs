namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Номер запрещен</i>
/// </summary>
public class NumberForbiddenError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 252;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Номер запрещен";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Возникает, например, при попытке указания Клиентом одного из наших федеральных номеров в " +
        "качестве получателя SMS-сообщения.";
}
