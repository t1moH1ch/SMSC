namespace SMSC.Responses.StatusCodes;

/// <summary>
/// Статус для <i>Остановлено</i>
/// </summary>
public class Stopped : IStatusCode
{
    /// <summary>
    /// Код статуса
    /// </summary>
    public int Code => -2;
    /// <summary>
    /// Название статуса
    /// </summary>
    public string Name => "Остановлено";
    /// <summary>
    /// Описание статуса
    /// </summary>
    public string Description => "Возникает у сообщений из рассылки, которые не успели уйти оператору до момента " +
        "временной остановки данной рассылки на странице Рассылки и задания.";
}
