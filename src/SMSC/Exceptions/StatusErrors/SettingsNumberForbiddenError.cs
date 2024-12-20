namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Номер запрещен настройками</i>
/// </summary>
public class SettingsNumberForbiddenError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 250;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Номер запрещен настройками";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Номер попал под ограничения, установленные Клиентом для мобильных номеров в личном кабинете " +
        "в пункте \"Настройки\".";
}
