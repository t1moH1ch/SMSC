namespace SMSC.Types;

/// <summary>
/// Конфигурация выделенных номеров для приёма сообщений (<c>receive_phones.php</c>).
/// </summary>
public class ReceivePhonesConfiguration : IConfiguration
{
    public ReceivePhonesOperation Operation { get; set; } = ReceivePhonesOperation.Get;
    public string? Phone { get; set; }
    public bool NoProlong { get; set; }
    public ResponseFormat ResponseFormat { get; set; } = ResponseFormat.Json;
    public Charset Charset { get; set; } = Charset.UTF8;
}
