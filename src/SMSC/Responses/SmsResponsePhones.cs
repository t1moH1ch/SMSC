#if NET_FRAMEWORK_AND_NET_STANDARD
using Newtonsoft.Json;
#endif

namespace SMSC.Responses;

/// <summary>
/// Структура ответа для запроса списка телефонных номеров. Для каждого номера будет записана данная структура
/// </summary>
public class SmsResponsePhones
{
    /// <summary>
    /// Номер телефона.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("phone")]
#else
    [JsonProperty(PropertyName = "phone")]
#endif
    public string? Phone { get; set; }
    /// <summary>
    /// Числовой код страны абонента плюс числовой код оператора абонента.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("mccmnc"), JsonConverter(typeof(NumberToStringJsonConverter))]
#else
    [JsonProperty(PropertyName = "mccmnc"), JsonConverter(typeof(NumberToStringJsonConverter))]
#endif
    public string? MccMnc { get; set; }
    /// <summary>
    /// Стоимость SMS-сообщения.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("cost"), JsonConverter(typeof(FloatFromStringJsonConverter))]
#else
    [JsonProperty(PropertyName = "cost"), JsonConverter(typeof(FloatFromStringJsonConverter))]
#endif
    public float? Cost { get; set; }
    /// <summary>
    /// Код статуса SMS-сообщения.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("status"), JsonConverter(typeof(StringToStatusJsonConverter))]
#else
    [JsonProperty(PropertyName = "status"), JsonConverter(typeof(StringToStatusJsonConverter))]
#endif
    public IStatusCode? Status { get; set; }
    /// <summary>
    /// Код ошибки в статусе.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("error"), JsonConverter(typeof(StringToErrorCodeJsonConverter))]
#else
    [JsonProperty(PropertyName = "error"), JsonConverter(typeof(StringToErrorCodeJsonConverter))]
#endif
    public ISmsError? Error { get; set; }
}
