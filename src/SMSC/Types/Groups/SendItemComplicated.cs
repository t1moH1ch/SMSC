namespace SMSC.Types.Groups;

/// <summary>
/// Структура для составления списка номеров в запросе
/// </summary>
/// <param name="sender">Имя отправителя</param>
/// <param name="phone">Номер телефона с + или без</param>
/// <param name="message">Сообщение</param>
public record SendItemComplicated(
    string sender,
    string phone,
    string message) : SendItem(phone, message)
{
    /// <summary>
    /// Имя отправителя зарегистрированное на <see href="https://smsc.ru/senders/">этой странице</see>
    /// </summary>
    public string Sender { get; } = sender;

    /// <summary>
    /// Часовой пояс абонента <see cref="SendItem.Phone"/><br/>
    /// <i>Параметр необязательный</i>
    /// </summary>
    public int? TimeZone { get; set; }
}