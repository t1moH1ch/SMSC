namespace SMSC.Types;

/// <summary>
/// Конфигурация управления субклиентами (<c>users.php</c>).
/// </summary>
public class UsersConfiguration : IConfiguration
{
    public UsersOperation Operation { get; set; } = UsersOperation.GetStat;
    public string? User { get; set; }
    public string? UserId { get; set; }
    public string? Password { get; set; }
    public string? NewLogin { get; set; }
    public string? Fio { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Sum { get; set; }
    public string? Start { get; set; }
    public string? End { get; set; }
    public string? Sender { get; set; }
    public bool Reseller { get; set; }
    public bool Balance2 { get; set; }
    public bool OwnCurrency { get; set; }
    public bool AllowOwnSender { get; set; }
    public int? Type { get; set; }
    public ResponseFormat ResponseFormat { get; set; } = ResponseFormat.Json;
    public Charset Charset { get; set; } = Charset.UTF8;
}
