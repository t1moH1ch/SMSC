namespace SMSC.Types;

public class SmsStatusConfiguration : IConfiguration
{
    /// <summary>
    /// Идентификатор сообщения. Назначается Клиентом. 
    /// Служит для дальнейшей идентификации сообщения. 
    /// Если не указывать, то будет назначен автоматически. Не обязательно уникален. 
    /// Идентификатор представляет собой 32-битное число в диапазоне от 1 до 2147483647, 
    /// либо строку длиной до 40 символов, состоящую из латинских букв, цифр и символов ".-_"
    /// </summary>
    public required string? Id { get; set; }
    /// <summary>
    /// Формат ответа сервера об успешной отправке.<br/>
    /// По умолчанию <see cref="ResponseFormat.Default"/>
    /// </summary>
    public ResponseFormat ResponseFormat { get; set; } = ResponseFormat.Json;
    /// <summary>
    /// Информация, котороую содержит возвращаемое сообщение<br/>
    /// Значение по умолчанию - <see cref="StatusType.Default"/>
    /// </summary>
    public StatusType StatusType { get; set; } = StatusType.Default;
    /// <summary>
	/// Кодировка переданного сообщения.<br/>
    /// По умолчанию <see cref="Charset.WIN1251"/>
	/// </summary>
	public Charset Charset { get; set; } = Charset.UTF8;
    /// <summary>
    /// Удаление сообщения.<br/>
    /// <see langword="false"/> - (по умолчанию) не удалять сообщение.<br/>
    /// <see langword="true"/> - удалить ранее отправленное сообщение. 
    /// Используется совместно с параметрами Phone и <see cref="Id"/>. Более подробно данный параметр описан 
    /// <seealso href="https://smsc.ru/api/http/status_messages/delete/#menu">здесь</seealso>.
    /// </summary>
    public bool Delete { get; set; }
}
