namespace SMSC.Responses;

public class HttpJobsResponse
{
    [JsonPropertyName("id"), JsonConverter(typeof(NumberToStringJsonConverter))]
    public string? Id { get; set; }

    [JsonPropertyName("cnt")]
    public int? Count { get; set; }

    [JsonPropertyName("cost"), JsonConverter(typeof(FloatFromStringJsonConverter))]
    public float? Cost { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("status")]
    public int? Status { get; set; }

    [JsonPropertyName("result")]
    public string? Result { get; set; }
}
