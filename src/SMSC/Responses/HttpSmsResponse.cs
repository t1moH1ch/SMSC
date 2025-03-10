#if NET_FRAMEWORK_AND_NET_STANDARD
using Newtonsoft.Json;
#endif

namespace SMSC.Responses;

/// <summary>
/// Структура ответа сервера
/// </summary>
public class HttpSmsResponse
{
    /// <summary>
    /// Идентификатор сообщения, переданный Клиентом или назначенный Сервером автоматически.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("id"), JsonConverter(typeof(NumberToStringJsonConverter))]
#else
    [JsonProperty(PropertyName = "id"), JsonConverter(typeof(NumberToStringJsonConverter))]
#endif
    public string? Id { get; set; }
    /// <summary>
    /// Количество частей (при отправке SMS-сообщения) либо количество секунд (при голосовом сообщении (звонке)).
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("cnt")]
#else
    [JsonProperty(PropertyName = "cnt")]
#endif
    public int? SmsCount { get; set; }
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
    /// Новый баланс Клиента.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("balance"), JsonConverter(typeof(FloatFromStringJsonConverter))]
#else
    [JsonProperty(PropertyName = "balance"), JsonConverter(typeof(FloatFromStringJsonConverter))]
#endif
    public float? Balance { get; set; }
    /// <summary>
    /// Подробный список с расшифровкой данных по каждому номеру клиента
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("phones")]
#else
    [JsonProperty(PropertyName = "phones")]
#endif
    public IEnumerable<SmsResponsePhones>? Phones { get; set; }
}
