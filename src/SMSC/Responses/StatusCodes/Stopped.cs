namespace SMSC.Responses.StatusCodes;

public class Stopped : IStatusCode
{
    public int Code => -2;

    public string Name => "Остановлено";

    public string Description => """
        Возникает у сообщений из рассылки, которые не успели уйти оператору до момента временной остановки данной рассылки на странице Рассылки и задания.
        """;
}
