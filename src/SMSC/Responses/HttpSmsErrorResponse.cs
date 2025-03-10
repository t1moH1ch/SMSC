#if NET_FRAMEWORK_AND_NET_STANDARD
using Newtonsoft.Json;
#endif

namespace SMSC.Responses;

/// <summary>
/// Структура ответа в случае ошибки
/// </summary>
public class HttpSmsErrorResponse
{
    /// <summary>
    /// Описание
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("error")]
#else
    [JsonProperty(PropertyName = "error")]
#endif
    public string? Error { get; set; }
    /// <summary>
    /// Номер ошибки, ознакомиться со всеми ошибками можно на <seealso href="https://smsc.ru/api/#menu">странице</seealso>
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("error_code")]
#else
    [JsonProperty(PropertyName = "error_code")]
#endif
    public int? ErrorCode { get; set; }
    /// <summary>
    /// Id сообщения
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("id"), JsonConverter(typeof(NumberToStringJsonConverter))]
#else
    [JsonProperty(PropertyName = "id"), JsonConverter(typeof(NumberToStringJsonConverter))]
#endif
    public string? Id { get; set; }
}
