namespace SMSC.Exceptions.StatusErrors;

public class InvalidNumberFormatError : ISmsError
{
    public int Code => 249;

    public string Name => "Неверный формат номера";

    public string Description => """
        Возникает, когда мобильный код указанного номера и соответствующая этому коду длина номера неверны.
        """;
}
