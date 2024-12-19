namespace SMSC.Exceptions;

/// <summary>
/// абстракция класса ощей ошибки, формируемой сервисом в ответ на запрос
/// </summary>
public abstract class SmsExceptionAbstract : Exception
{
    /// <summary>
    /// Номер ошибки в соответствии с <seealso href="https://smsc.ru/api/#menu">документацией</seealso>
    /// </summary>
    public abstract int Id { get; }
}
