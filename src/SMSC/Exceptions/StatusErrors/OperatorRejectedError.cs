namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Отклонено оператором</i>
/// </summary>
public class OperatorRejectedError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 255;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Отклонено оператором";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Оператор отклонил сообщение без указания точного кода ошибки. " +
        "Такое бывает, например, когда номер не принадлежит ни одному мобильному оператору, т.е. с несуществующим кодом, " +
        "либо по какой-то другой причине оператор не может доставить сообщение.";
}
