namespace SMSC.Responses;

/// <summary>
/// Структура ответа в случае ошибки
/// </summary>
public class HttpSmsErrorResponse
{
    /// <summary>
    /// Описание
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }
    /// <summary>
    /// Номер ошибки, ознакомиться со всеми ошибками можно на <seealso href="https://smsc.ru/api/#menu">странице</seealso>
    /// </summary>
    [JsonPropertyName("error_code")]
    public int? ErrorCode { get; set; }
    /// <summary>
    /// Id сообщения
    /// </summary>
    [JsonPropertyName("id"), JsonConverter(typeof(NumberToStringJsonConverter))]
    public string? Id { get; set; }
}
