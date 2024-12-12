namespace SMSC.Types.Enums;

/// <summary>
/// Признак бинарного сообщения
/// </summary>
public enum BinaryMessage
{
    /// <summary>
    /// обычное сообщение
    /// </summary>
    Default,
    /// <summary>
    /// бинарное сообщение. В http-запросе необходимо закодировать с помощью функции urlencode
    /// </summary>
    Http,
    /// <summary>
    /// бинарное сообщение, представленное в виде шестнадцатеричной строки (hex)
    /// </summary>
    Hex
}
