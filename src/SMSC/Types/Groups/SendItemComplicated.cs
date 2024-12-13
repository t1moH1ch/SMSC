namespace SMSC.Types.Groups;

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