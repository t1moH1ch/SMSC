namespace SMSC.Exceptions.HttpSms;

public class HttpSmsTooManyConcurrentRequestsException : SmsExceptionAbstract
{
    /// <summary>
    /// Номер ошибки в соответствии с <seealso href="https://smsc.ru/api/#menu">документацией</seealso>
    /// </summary>
    public override int Id { get; } = 9;
    /// <summary>
    /// Отправка более одного одинакового запроса на передачу SMS-сообщения либо более пяти одинаковых 
    /// запросов на получение стоимости сообщения в течение минуты.<br/>
    /// Данная ошибка возникает также при попытке отправки пятнадцати и более запросов одновременно с разных 
    /// подключений под одним логином.
    /// </summary>
    public override string Message => "Sending more than one identical SMS message request or more than five identical SMS cost requests within a minute.";
}
