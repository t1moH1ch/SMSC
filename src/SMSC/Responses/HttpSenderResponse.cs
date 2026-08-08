namespace SMSC.Responses;

public class HttpSenderResponse
{
    [JsonPropertyName("id"), JsonConverter(typeof(NumberToStringJsonConverter))]
    public string? Id { get; set; }

    [JsonPropertyName("sender")]
    public string? Sender { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("status")]
    public int? Status { get; set; }

    [JsonPropertyName("result")]
    public string? Result { get; set; }

    [JsonPropertyName("cmt")]
    public string? Comment { get; set; }
}
