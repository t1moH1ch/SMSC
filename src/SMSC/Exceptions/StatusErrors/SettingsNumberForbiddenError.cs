namespace SMSC.Exceptions.StatusErrors;

public class SettingsNumberForbiddenError : ISmsError
{
    public int Code => 250;

    public string Name => "Номер запрещен настройками";

    public string Description => """
        Номер попал под ограничения, установленные Клиентом для мобильных номеров в личном кабинете в пункте "Настройки".
        """;
}
