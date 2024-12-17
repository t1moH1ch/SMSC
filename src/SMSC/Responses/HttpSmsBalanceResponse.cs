namespace SMSC.Responses;

/// <summary>
/// Структура ответа для запроса баланса
/// </summary>
public class HttpSmsBalanceResponse
{
    /// <summary>
    /// Текущее состояние баланса.
    /// </summary>
    [JsonPropertyName("balance"), JsonConverter(typeof(FloatFromStringJsonConverter))]
    public float? Balance { get; set; }
    /// <summary>
    /// Текущее состояние установленного кредита.
    /// </summary>
    [JsonPropertyName("credit"), JsonConverter(typeof(FloatFromStringJsonConverter))]
    public float? Credit { get; set; }
    /// <summary>
    /// Валюта Клиента.
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }
}
