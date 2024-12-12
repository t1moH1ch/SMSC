namespace SMSC.Exceptions.HttpSms;

public class HttpSmsFundLimitException : SmsExceptionAbstract
{
    /// <summary>
    /// Номер ошибки в соответствии с <seealso href="https://smsc.ru/api/#menu">документацией</seealso>
    /// </summary>
    public override int Id { get; } = 3;
    /// <summary>
    /// Недостаточно средств на счете Клиента.
    /// </summary>
    public override string Message => "There are not enough funds in the Client's account.";
}
