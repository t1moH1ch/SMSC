namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Нет ошибки</i>
/// </summary>
public class NoError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 0;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Нет ошибки";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Получатель существует и доступен.";
}
