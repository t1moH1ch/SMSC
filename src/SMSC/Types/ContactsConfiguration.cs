namespace SMSC.Types;

/// <summary>
/// Конфигурация операций с контактами и группами (<c>phones.php</c>).
/// </summary>
public class ContactsConfiguration : IConfiguration
{
    public ContactOperation Operation { get; set; } = ContactOperation.Get;
    public string? Phone { get; set; }
    public string? NewPhone { get; set; }
    public string? Name { get; set; }
    /// <summary>ID группы (grp).</summary>
    public string? GroupId { get; set; }
    /// <summary>Тип для чёрного списка (type).</summary>
    public int? BlackListType { get; set; }
    public ResponseFormat ResponseFormat { get; set; } = ResponseFormat.Json;
    public Charset Charset { get; set; } = Charset.UTF8;
}
