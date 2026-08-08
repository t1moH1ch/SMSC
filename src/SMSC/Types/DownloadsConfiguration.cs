namespace SMSC.Types;

/// <summary>
/// Конфигурация отложенных задач выгрузки (<c>downloads.php</c>).
/// </summary>
public class DownloadsConfiguration : IConfiguration
{
    public DownloadsOperation Operation { get; set; } = DownloadsOperation.GetList;
    public string? Id { get; set; }
    public string? Name { get; set; }
    public int? Count { get; set; }
    public string? AfterId { get; set; }
    public ResponseFormat ResponseFormat { get; set; } = ResponseFormat.Json;
    public Charset Charset { get; set; } = Charset.UTF8;
}
