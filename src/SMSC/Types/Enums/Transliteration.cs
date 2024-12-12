namespace SMSC.Types.Enums;

/// <summary>
/// Признак того, что сообщение необходимо перевести в транслит
/// </summary>
public enum Transliteration
{
    /// <summary>
    /// (по умолчанию) – не переводить в транслит
    /// </summary>
    Default,
    /// <summary>
    /// Перевести в транслит в виде "translit"
    /// </summary>
    Translit,
    /// <summary>
    /// перевести в транслит в виде "mpaHc/Ium"
    /// </summary>
    TranslitExtend
}
