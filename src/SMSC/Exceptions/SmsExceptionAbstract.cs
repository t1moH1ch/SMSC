namespace SMSC.Exceptions;
public abstract class SmsExceptionAbstract : Exception
{
    /// <summary>
    /// Номер ошибки в соответствии с <seealso href="https://smsc.ru/api/#menu">документацией</seealso>
    /// </summary>
    public abstract int Id { get; }
}
