namespace SMSC.Responses;

public class HttpContactResponse
{
    [JsonPropertyName("id"), JsonConverter(typeof(NumberToStringJsonConverter))]
    public string? Id { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("grp"), JsonConverter(typeof(NumberToStringJsonConverter))]
    public string? GroupId { get; set; }

    [JsonPropertyName("result")]
    public string? Result { get; set; }

    [JsonPropertyName("cnt")]
    public int? Count { get; set; }
}
