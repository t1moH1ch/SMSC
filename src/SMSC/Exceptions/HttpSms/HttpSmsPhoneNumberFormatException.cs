namespace SMSC.Exceptions.HttpSms;

public class HttpSmsPhoneNumberFormatException : SmsExceptionAbstract
{
    /// <summary>
    /// Номер ошибки в соответствии с <seealso href="https://smsc.ru/api/#menu">документацией</seealso>
    /// </summary>
    public override int Id { get; } = 7;
    /// <summary>
    /// Неверный формат номера телефона.
    /// </summary>
    public override string Message => "The phone number format is incorrect.";
}
