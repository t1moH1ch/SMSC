namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Ошибка конвертации</i>
/// </summary>
public class ConversionError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 241;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Ошибка конвертации";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "При преобразовании текста или звукового файла в конечный формат для передачи абоненту голосового сообщения " +
        "произошла ошибка конвертации звука. Также данная ошибка может возникать при передаче SMS-сообщения отдельными " +
        "частями, когда оператору переданы не все части.";
}
