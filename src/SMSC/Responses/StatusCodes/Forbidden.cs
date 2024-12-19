namespace SMSC.Responses.StatusCodes;

public class Forbidden : IStatusCode
{
    public int Code => 23;

    public string Name => "Запрещено";

    public string Description => """
        Возникает при срабатывании ограничений на отправку дублей, на частые сообщения на один номер (флуд), 
        на номера из черного списка, на запрещенные спам фильтром тексты или имена отправителей (Sender ID).
        """;
}
