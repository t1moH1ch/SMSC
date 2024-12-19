namespace SMSC.Types.Enums;

/// <summary>
/// Список доступных параметров для конфигурации голоса запроса в структуре <see cref="CallConfiguration"/> 
/// при <see cref="SmsConfiguration.SmsType"/> = <see cref="SmsType.Call"/>.
/// </summary>
public enum Voice
{
    /// <summary>
    /// мужской голос
    /// </summary>
    Male1,
    /// <summary>
    /// мужской голос 2
    /// </summary>
    Male2,
    /// <summary>
    /// мужской голос 3
    /// </summary>
    Male3,
    /// <summary>
    /// мужской голос 4
    /// </summary>
    Male4,
    /// <summary>
    /// женский голос 1
    /// </summary>
    Female1,
    /// <summary>
    /// женский голос 2
    /// </summary>
    Female2,
    /// <summary>
    /// женский голос 3
    /// </summary>
    Female3,
    /// <summary>
    /// женский голос 4
    /// </summary>
    Female4
}