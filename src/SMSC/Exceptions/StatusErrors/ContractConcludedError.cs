namespace SMSC.Exceptions.StatusErrors;

public class ContractConcludedError : ISmsError
{
    public int Code => 243;
    public string Name => "Не заключен договор";
    public string Description => """
        Возникает при попытке отправки рассылок рекламного или массового характера без заключенного договора.
        """;
}
