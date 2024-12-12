namespace SMSC.Exceptions.HttpSms;

public class HttpSmsDateFormatException : SmsExceptionAbstract
{
    /// <summary>
    /// Номер ошибки в соответствии с <seealso href="https://smsc.ru/api/#menu">документацией</seealso>
    /// </summary>
    public override int Id { get; } = 5;
    /// <summary>
    /// Неверный формат даты.
    /// </summary>
    public override string Message => "The date format is incorrect.";
}
