namespace SMSC.Types.Enums;

/// <summary>
/// Признак необходимости получения стоимости рассылки.
/// </summary>
public enum Cost
{
    /// <summary>
    /// (по умолчанию) – обычная отправка
    /// </summary>
    Default,
    /// <summary>
    /// получить стоимость рассылки без реальной отправки
    /// </summary>
    Cost,
    /// <summary>
    /// обычная отправка, но добавить в ответ стоимость выполненной рассылки
    /// </summary>
    CostSend,
    /// <summary>
    /// обычная отправка, но добавить в ответ стоимость и новый баланс Клиента
    /// </summary>
    CostAndBalance
}
