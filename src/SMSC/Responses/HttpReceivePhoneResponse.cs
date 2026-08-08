namespace SMSC.Responses;

public class HttpReceivePhoneResponse
{
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("type")]
    public int? Type { get; set; }

    [JsonPropertyName("cost"), JsonConverter(typeof(FloatFromStringJsonConverter))]
    public float? Cost { get; set; }

    [JsonPropertyName("current_cost"), JsonConverter(typeof(FloatFromStringJsonConverter))]
    public float? CurrentCost { get; set; }

    [JsonPropertyName("info")]
    public string? Info { get; set; }

    [JsonPropertyName("result")]
    public string? Result { get; set; }
}
