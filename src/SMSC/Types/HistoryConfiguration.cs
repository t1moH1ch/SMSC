namespace SMSC.Types;

/// <summary>
/// Конфигурация получения истории и статистики (<c>get.php</c>).
/// </summary>
public class HistoryConfiguration : IConfiguration
{
    public HistoryOperation Operation { get; set; } = HistoryOperation.GetMessages;
    public string? Start { get; set; }
    public string? End { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? ToPhone { get; set; }
    public int? Format { get; set; }
    public int? Count { get; set; }
    public string? PrevId { get; set; }
    public string? AfterId { get; set; }
    public int? Hour { get; set; }
    public bool OwnCurrency { get; set; }
    public bool Balance2 { get; set; }
    public ResponseFormat ResponseFormat { get; set; } = ResponseFormat.Json;
    public Charset Charset { get; set; } = Charset.UTF8;
}
