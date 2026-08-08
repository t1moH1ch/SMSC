namespace SMSC.Responses;

public class HttpUsersResponse
{
    [JsonPropertyName("id"), JsonConverter(typeof(NumberToStringJsonConverter))]
    public string? Id { get; set; }

    [JsonPropertyName("user")]
    public string? User { get; set; }

    [JsonPropertyName("fio")]
    public string? Fio { get; set; }

    [JsonPropertyName("result")]
    public string? Result { get; set; }

    [JsonPropertyName("balance"), JsonConverter(typeof(FloatFromStringJsonConverter))]
    public float? Balance { get; set; }

    [JsonPropertyName("sum"), JsonConverter(typeof(FloatFromStringJsonConverter))]
    public float? Sum { get; set; }
}
