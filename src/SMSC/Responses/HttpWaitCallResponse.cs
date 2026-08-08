namespace SMSC.Responses;

public class HttpWaitCallResponse
{
    /// <summary>
    /// Номер, с которого нужно совершить звонок в течение 15 минут.
    /// </summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    /// <summary>
    /// Список всех выделенных номеров для подтверждения (для отображения абоненту).
    /// </summary>
    [JsonPropertyName("all_phones")]
    public string[]? AllPhones { get; set; }
}
