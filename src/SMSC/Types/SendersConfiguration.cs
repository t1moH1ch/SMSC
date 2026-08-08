namespace SMSC.Types;

/// <summary>
/// Конфигурация управления именами отправителей (<c>senders.php</c>).
/// </summary>
public class SendersConfiguration : IConfiguration
{
    public SendersOperation Operation { get; set; } = SendersOperation.Get;
    public string? Sender { get; set; }
    public string? Id { get; set; }
    public string? Comment { get; set; }
    public string? Code { get; set; }
    public string? Site { get; set; }
    public string? SiteName { get; set; }
    public string? User { get; set; }
    public bool All { get; set; }
    public bool Operators { get; set; }
    public bool Mail { get; set; }
    public string? Name { get; set; }
    public int? Count { get; set; }
    public string? PrevId { get; set; }
    public string? FromDate { get; set; }
    public ResponseFormat ResponseFormat { get; set; } = ResponseFormat.Json;
    public Charset Charset { get; set; } = Charset.UTF8;
}
