namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Запрещенный ip-адрес</i>
/// </summary>
public class ForbiddenIpAddressError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 239;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Запрещенный ip-адрес";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Возникает при попытке отправки сообщения с ip-адреса, не входящего в список ip-адресов, " +
        "разрешенных Клиентом для отправки. Также может возникать при попытке отправки сообщения с ip-адреса, " +
        "ранее не используемого для отправки сообщений и входов в личный кабинет.";
}
