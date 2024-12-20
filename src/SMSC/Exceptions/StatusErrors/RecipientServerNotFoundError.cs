namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Сервер получателя не найден</i>
/// </summary>
public class RecipientServerNotFoundError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 3;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Сервер получателя не найден";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Домен, указанный в качестве почтового сервера, не найден.";
}
