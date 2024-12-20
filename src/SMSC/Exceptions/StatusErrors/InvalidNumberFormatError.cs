namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Неверный формат номера</i>
/// </summary>
public class InvalidNumberFormatError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 249;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Неверный формат номера";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Возникает, когда мобильный код указанного номера и соответствующая этому коду длина номера неверны.";
}
