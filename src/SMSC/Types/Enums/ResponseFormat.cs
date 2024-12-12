namespace SMSC.Types.Enums;

/// <summary>
/// Формат ответа сервера об успешной отправке.
/// </summary>
public enum ResponseFormat
{
    /// <summary>
    /// (по умолчанию) в виде строки (OK - 1 SMS, ID - 1234).
    /// </summary>
    Default,
    /// <summary>
    /// Вернуть ответ в виде чисел: ID и количество SMS через запятую (1234,1),<br/>
    /// при cost = 2 еще стоимость через запятую (1234,1,1.40), <br/>
    /// при cost = 3 еще новый баланс Клиента (1234,1,1.40,100.50), <br/>
    /// при cost = 1 стоимость и количество SMS через запятую (1.40,1).
    /// </summary>
    Digits,
    /// <summary>
    /// Ответ в xml формате.
    /// </summary>
    XML,
    /// <summary>
    /// Ответ в json формате.
    /// </summary>
    Json
}
