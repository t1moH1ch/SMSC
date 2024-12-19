namespace SMSC.Exceptions.StatusErrors;

public class OperatorRejectedError : ISmsError
{
    public int Code => 255;

    public string Name => "Отклонено оператором";

    public string Description => """
        Оператор отклонил сообщение без указания точного кода ошибки.
        Такое бывает, например, когда номер не принадлежит ни одному мобильному оператору, т.е. с несуществующим кодом, 
        либо по какой-то другой причине оператор не может доставить сообщение.
        """;
}
