namespace SMSC.Types;

public class CallConfiguration
{
    private int _wait, _repeatSpan, _retryCount;

    /// <summary>
    /// Голос, используемый для озвучивания текста (только для голосовых сообщений).<br/>
    /// На данный момент существует поддержка 4 мужских и 4 женских голосов. Для отправки 
    /// голосового сообщения на определенном языке укажите <seealso cref="VoiceLanguage"/>.<br/>
    /// Значение по умлочанию <see cref="Voice.Male3"/>
    /// </summary>
    public Voice Voice { get; set; } = Voice.Male3;
    /// <summary>
    /// Отправка голосового сообщения на заданном языке. Формат: "en", "fr", "de" и т.п.
    /// Используется только при <see cref="SmsType.Call"/>
    /// </summary>
    public string? VoiceLanguage { get; set; }

    /// <summary>
    /// Время ожидания поднятия трубки абонентом после начала звонка в секундах. 
    /// Если в течение указанного времени абонент не поднимет трубку, то звонок уйдет на повтор с ошибкой "абонент занят". 
    /// Рабочий диапазон значений параметра от 10 до 35, но можно указывать интервал от 0 до 99.<br/>
    /// Значение по умолчанию - 22
    /// </summary>
    public int Wait
    {
        get => _wait;
        set
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(value, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 99);

            _wait = value;
        }
    }
    /// <summary>
    /// Интервал повтора, то есть промежуток времени, по истечении которого произойдет повторный звонок (в секундах). 
    /// Рабочий диапазон параметра от 10 до 3600.<br/>
    /// Значение по умолчанию - 20
    /// </summary>
    public int RepeatSpan
    {
        get => _repeatSpan;
        set
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(value, 10);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 3600);

            _repeatSpan = value;
        }
    }
    /// <summary>
    /// Общее количество попыток дозвона. Рабочий диапазон значений от 1 до 9.<br/>
    /// Значение по умолчанию - 5
    /// </summary>
    public int RetryCount
    {
        get => _retryCount;
        set
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(value, 1);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 9);

            _retryCount = value;
        }
    }

    public CallConfiguration()
    {
        Wait = 22;
        RepeatSpan = 20;
        RetryCount = 5;
    }
}
