namespace SMSC.Responses;

public class HttpHistoryMessageResponse
{
    [JsonPropertyName("id"), JsonConverter(typeof(NumberToStringJsonConverter))]
    public string? Id { get; set; }

    [JsonPropertyName("int_id"), JsonConverter(typeof(NumberToStringJsonConverter))]
    public string? InternalId { get; set; }

    [JsonPropertyName("status")]
    public int? Status { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("cost"), JsonConverter(typeof(FloatFromStringJsonConverter))]
    public float? Cost { get; set; }

    [JsonPropertyName("sender_id")]
    public string? Sender { get; set; }

    [JsonPropertyName("status_name")]
    public string? StatusName { get; set; }

    [JsonPropertyName("send_date")]
    public string? SendDate { get; set; }

    [JsonPropertyName("last_date")]
    public string? LastDate { get; set; }

    [JsonPropertyName("type")]
    public int? Type { get; set; }

    [JsonPropertyName("sms_cnt")]
    public int? SmsCount { get; set; }

    [JsonPropertyName("format")]
    public int? Format { get; set; }

    [JsonPropertyName("crc")]
    public string? Crc { get; set; }

    [JsonPropertyName("received")]
    public string? Received { get; set; }

    [JsonPropertyName("to_phone")]
    public string? ToPhone { get; set; }

    [JsonPropertyName("sent")]
    public string? Sent { get; set; }
}
