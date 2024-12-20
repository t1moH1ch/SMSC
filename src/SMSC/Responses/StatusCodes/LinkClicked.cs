namespace SMSC.Responses.StatusCodes;

/// <summary>
/// Статус для <i>Нажата ссылка</i>
/// </summary>
public class LinkClicked : IStatusCode
{
    /// <summary>
    /// Код статуса
    /// </summary>
    public int Code => 4;
    /// <summary>
    /// Название статуса
    /// </summary>
    public string Name => "Нажата ссылка";
    /// <summary>
    /// Описание статуса
    /// </summary>
    public string Description => "Сообщение было доставлено, и абонентом была нажата короткая ссылка, переданная в сообщении. " +
        "Данный статус возможен при включенных в настройках опциях \"Автоматически сокращать ссылки в сообщениях\" и " +
        "\"отслеживать номера абонентов\".";
}
