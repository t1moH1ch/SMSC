namespace SMSC.Types;

/// <summary>
/// Структура конфигурации для запроса баланса
/// </summary>
public class SmsBalanceConfiguration : IConfiguration
{
    /// <summary>
    /// Флаг, указывающий на необходимость добавления в ответ сервера названия валюты Клиента.
    /// </summary>
    public bool Currency { get; set; }
    /// <summary>
    /// Формат ответа сервера об успешной отправке.<br/>
    /// По умолчанию <see cref="ResponseFormat.Default"/>
    /// </summary>
    public ResponseFormat ResponseFormat { get; set; } = ResponseFormat.Json;
    /// <summary>
    /// Кодировка переданного сообщения.<br/>
    /// По умолчанию <see cref="Charset.UTF8"/>
    /// </summary>
    public Charset Charset { get; set; } = Charset.UTF8;
}
