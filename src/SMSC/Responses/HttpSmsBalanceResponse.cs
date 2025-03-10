#if NET_FRAMEWORK_AND_NET_STANDARD
using Newtonsoft.Json;
#endif

namespace SMSC.Responses;

/// <summary>
/// Структура ответа для запроса баланса
/// </summary>
public class HttpSmsBalanceResponse
{
    /// <summary>
    /// Текущее состояние баланса.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("balance"), JsonConverter(typeof(FloatFromStringJsonConverter))]
#else
    [JsonProperty(PropertyName = "balance"), JsonConverter(typeof(FloatFromStringJsonConverter))]
#endif
    public float? Balance { get; set; }
    /// <summary>
    /// Текущее состояние установленного кредита.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("credit"), JsonConverter(typeof(FloatFromStringJsonConverter))]
#else
    [JsonProperty(PropertyName = "credit"), JsonConverter(typeof(FloatFromStringJsonConverter))]
#endif
    public float? Credit { get; set; }
    /// <summary>
    /// Валюта Клиента.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("currency")]
#else
    [JsonProperty(PropertyName = "currency")]
#endif
    public string? Currency { get; set; }
}
