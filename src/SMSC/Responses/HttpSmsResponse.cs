namespace SMSC.Responses;

public class HttpSmsResponse
{
    /// <summary>
    /// Идентификатор сообщения, переданный Клиентом или назначенный Сервером автоматически.
    /// </summary>
    [JsonPropertyName("id"), JsonConverter(typeof(NumberToStringJsonConverter))]
    public string? Id { get; set; }
    /// <summary>
    /// Количество частей (при отправке SMS-сообщения) либо количество секунд (при голосовом сообщении (звонке)).
    /// </summary>
    [JsonPropertyName("cnt")]
    public int? SmsCount { get; set; }
    /// <summary>
    /// Стоимость SMS-сообщения.
    /// </summary>
    [JsonPropertyName("cost"), JsonConverter(typeof(FloatFromStringJsonConverter))]
    public float? Cost { get; set; }
    /// <summary>
    /// Новый баланс Клиента.
    /// </summary>
    [JsonPropertyName("balance"), JsonConverter(typeof(FloatFromStringJsonConverter))]
    public float? Balance { get; set; }
    /// <summary>
    /// Подробный список с расшифровкой данных по каждому номеру клиента
    /// </summary>
    [JsonPropertyName("phones")]
    public IEnumerable<SmsResponsePhones>? Phones { get; set; }
}
