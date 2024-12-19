namespace SMSC.Exceptions;

/// <summary>
/// Абстракция класса внутренней ошибки
/// </summary>
public interface ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    int Code { get; }
    /// <summary>
    /// Название ошибки
    /// </summary>
    string Name { get; }
    /// <summary>
    /// Описание ошибки
    /// </summary>
    string Description { get; }
}
