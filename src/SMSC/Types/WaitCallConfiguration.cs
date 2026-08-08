namespace SMSC.Types;

/// <summary>
/// Конфигурация запроса wait_call (подтверждение номера звонком).
/// </summary>
public class WaitCallConfiguration : IConfiguration
{
    public ResponseFormat ResponseFormat { get; set; } = ResponseFormat.Json;
    public Charset Charset { get; set; } = Charset.UTF8;
}
