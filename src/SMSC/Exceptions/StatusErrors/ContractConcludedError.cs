namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Не заключен договор</i>
/// </summary>
public class ContractConcludedError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 243;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Не заключен договор";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Возникает при попытке отправки рассылок рекламного или массового характера без заключенного договора.";
}
