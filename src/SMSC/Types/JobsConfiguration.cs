namespace SMSC.Types;

/// <summary>
/// Конфигурация рассылок (<c>jobs.php</c>).
/// </summary>
public class JobsConfiguration : IConfiguration
{
    public JobsOperation Operation { get; set; } = JobsOperation.Get;
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Phones { get; set; }
    public string? Message { get; set; }
    public string? Message2 { get; set; }
    public string? Sender { get; set; }
    public string? Sender2 { get; set; }
    public string? Subject { get; set; }
    public string? Time { get; set; }
    public string? Start { get; set; }
    public string? End { get; set; }
    public string? List { get; set; }
    public string? BotName { get; set; }
    public string? FileUrl { get; set; }
    public string? TemplateId { get; set; }
    public JobRepeat Repeat { get; set; } = JobRepeat.None;
    public int? RepeatCount { get; set; }
    public SmsType SmsType { get; set; } = SmsType.Default;
    public bool FallbackToSms { get; set; }
    public ResponseFormat ResponseFormat { get; set; } = ResponseFormat.Json;
    public Charset Charset { get; set; } = Charset.UTF8;
}
