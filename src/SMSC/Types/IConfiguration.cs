namespace SMSC.Types;

/// <summary>
/// Интерфейс для обощения конфигураций абстракции <see cref="HttpAbstract"/>
/// </summary>
public interface IConfiguration
{
    /// <summary>
    /// Формат ответа сервера об успешной отправке.<br/>
    /// По умолчанию <see cref="ResponseFormat.Default"/>
    /// </summary>
    ResponseFormat ResponseFormat { get; set; }
    /// <summary>
    /// Кодировка переданного сообщения.<br/>
    /// По умолчанию <see cref="Charset.UTF8"/>
    /// </summary>
    public Charset Charset { get; set; }
}
