namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Нет поддержки сервиса</i>
/// </summary>
public class NoServiceSupportError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 21;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Нет поддержки сервиса";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Аппарат абонента не поддерживает работу с данной услугой (сервисом).";
}
