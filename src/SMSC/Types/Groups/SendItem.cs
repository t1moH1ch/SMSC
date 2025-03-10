﻿namespace SMSC.Types.Groups;

/// <summary>
/// Класс для определения пары телефон-сообщение
/// </summary>
/// <param name="phone"></param>
/// <param name="message"></param>
#if NET_CORE_APP
public record SendItem(
#else
public class SendItem(
#endif
    string phone,
    string message)
{
    /// <summary>
    /// Номер телефона. Можно указать несколько номеров телефонов через запятую.
    /// </summary>
    public string Phone { get; } = phone;
    /// <summary>
    /// Сообщение для отправки на <see cref="Phone"/><br/>
    /// Если в сообщении mes требуется передать символ новой строки, то укажите его через \n. 
    /// В случае невозможности корректировки текста мультистрокового сообщения используйте <see cref="UseNewLineMode"/>.
    /// </summary>
    public string Message { get; } = message;

    /// <summary>
    /// Включение специального режима для передачи мультистрокового типа сообщений.<br/>
    /// <see langword="false"/> - (по умолчанию) - специальный режим выключен<br/>
    /// <see langword="true"/> - включение специального режима
    /// </summary>
    public bool UseNewLineMode { get => Message.Contains('\n'); }
}
