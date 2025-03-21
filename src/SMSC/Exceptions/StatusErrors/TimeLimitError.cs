﻿namespace SMSC.Exceptions.StatusErrors;

/// <summary>
/// Ошибка для <i>Ограничение по времени</i>
/// </summary>
public class TimeLimitError : ISmsError
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code => 246;
    /// <summary>
    /// Название ошибки
    /// </summary>
    public string Name => "Ограничение по времени";
    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Description => "Если в личном кабинете в пункте \"Настройки\" во вкладке \"Лимиты и ограничения\" установлено \"Время отправки\" и " +
        "галочка \"запретить отправку в другое время\", то при попытке отправки SMS-сообщений в период времени, отличный " +
        "от указанного в поле \"Время отправки\", отправка сообщений будет запрещаться с указанием данной ошибки.";
}
