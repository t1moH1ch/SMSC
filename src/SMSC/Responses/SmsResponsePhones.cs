namespace SMSC.Responses;

public class SmsResponsePhones
{
    /// <summary>
    /// Номер телефона.
    /// </summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }
    /// <summary>
    /// Числовой код страны абонента плюс числовой код оператора абонента.
    /// </summary>
    [JsonPropertyName("mccmnc"), JsonConverter(typeof(NumberToStringJsonConverter))]
    public string? MccMnc { get; set; }
    /// <summary>
    /// Стоимость SMS-сообщения.
    /// </summary>
    [JsonPropertyName("cost"), JsonConverter(typeof(FloatFromStringJsonConverter))]
    public float? Cost { get; set; }
    /// <summary>
    /// Код статуса SMS-сообщения.
    /// </summary>
    [JsonPropertyName("status"), JsonConverter(typeof(StringToStatusJsonConverter))]
    public IStatusCode? Status { get; set; }
    /// <summary>
    /// Код ошибки в статусе.
    /// </summary>
    [JsonPropertyName("error"), JsonConverter(typeof(StringToErrorCodeJsonConverter))]
    public ISmsError? Error { get; set; }
}
