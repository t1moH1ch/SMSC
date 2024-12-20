namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Нет шаблона</i>
/// </summary>
public class NoTemplateError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 238;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Нет шаблона";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Возникает, если отправка сообщения возможна только по определенному шаблону, а отправляемое " +
        "сообщение ему не удовлетворяет.";
}
