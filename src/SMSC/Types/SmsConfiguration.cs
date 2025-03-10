namespace SMSC.Types;

/// <summary>
/// Структура конфигурации для отправки СМС сообщений
/// </summary>
public class SmsConfiguration : IConfiguration
{
    private int? _smsRequire;
    private TimeSpan? _valid, _period, _frequency;

    /// <summary>
    /// Идентификатор сообщения. Назначается Клиентом. 
    /// Служит для дальнейшей идентификации сообщения. 
    /// Если не указывать, то будет назначен автоматически. Не обязательно уникален. 
    /// Идентификатор представляет собой 32-битное число в диапазоне от 1 до 2147483647, 
    /// либо строку длиной до 40 символов, состоящую из латинских букв, цифр и символов ".-_"
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// Имя отправителя, отображаемое в телефоне получателя. 
    /// Разрешены английские буквы, цифры, пробел и некоторые символы. 
    /// Длина – 11 символов или 15 цифр. 
    /// Все имена регистрируются в личном кабинете на <seealso href="https://smsc.ru/senders/">данной</seealso> странице
    /// </summary>
    public string? Sender { get; set; }

    /// <summary>
    /// Признак того, что сообщение необходимо перевести в транслит
    /// </summary>
    public Transliteration Transliteration { get; set; } = Transliteration.Default;
    /// <summary>
    /// Автоматически сокращать ссылки в сообщениях. 
    /// Позволяет заменять ссылки в тексте сообщения на короткие для сокращения длины, 
    /// а также для отслеживания количества переходов на <seealso href="https://smsc.ru/tinyurls/">этой странице</seealso>.<br/>
    /// <see langword="true"/> - оставить ссылки в тексте сообщения без изменений<br/>
    /// <see langword="false"/> - сократить ссылки
    /// </summary>
    public bool TinyUrl { get; set; }

    /// <summary>
    /// Формат времени отправки SMS-сообщения абоненту
    /// </summary>
    public TimeFormat Time { get; set; } = TimeFormat.Now;
    /// <summary>
    /// Времени отправки SMS-сообщения абоненту
    /// </summary>
    public string? TimeToSend { get; set; }

    /// <summary>
    /// Часовой пояс, в котором задается параметр <see cref="Time"/>. 
    /// Указывается относительно московского времени. 
    /// Параметр <see cref="TimeZone"/> может быть как положительным, так и отрицательным. Если <see cref="TimeZone"/> равен 0, 
    /// то будет использован московский часовой пояс, если же параметр <see cref="TimeZone"/> не задан, 
    /// то часовой пояс будет взят из настроек Клиента
    /// </summary>
    public int? TimeZone { get; set; }
    /// <summary>
    /// Промежуток времени, в течение которого необходимо отправить рассылку. 
    /// Представляет собой число в диапазоне от 0.1 до 720 часов. 
    /// Применяется совместно с параметром <see cref="Frequency"/>. Данный параметр позволяет растянуть рассылку 
    /// во времени для постепенного получения SMS-сообщений абонентами.<br/>
    /// Значение по умолчанию 24 часа.
    /// </summary>
    public TimeSpan? Period
    {
        get => _period;
        set
        {
            if (value is not null)
            {
#if NET_CORE_APP_8
                ArgumentOutOfRangeException.ThrowIfLessThan(value.Value, TimeSpan.FromMinutes(6));
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Value, TimeSpan.FromHours(720));
#else
                if (value.Value < TimeSpan.FromMinutes(6))
                    throw new ArgumentOutOfRangeException(nameof(value.Value));
                if (value.Value > TimeSpan.FromHours(720))
                    throw new ArgumentOutOfRangeException(nameof(value.Value));
#endif
            }

            _period = value;
        }
    }
    /// <summary>
    /// Интервал или частота, с которой нужно отправлять SMS-рассылку на очередную группу номеров. 
    /// Количество номеров в группе рассчитывается автоматически на основе параметров <see cref="Period"/> и <see cref="Frequency"/>. 
    /// Задается в промежутке от 1 до 1440 минут. Без параметра <see cref="Period"/> параметр <see cref="Frequency"/> игнорируется.<br/>
    /// Значение по умолчанию 60 минут.
    /// </summary>
    public TimeSpan? Frequency
    {
        get => _frequency;
        set
        {
            if (value is not null)
            {
#if NET_CORE_APP_8
                ArgumentOutOfRangeException.ThrowIfLessThan(value.Value, TimeSpan.FromMinutes(1));
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Value, TimeSpan.FromMinutes(1440));
#else
                if (value.Value < TimeSpan.FromMinutes(1))
                    throw new ArgumentOutOfRangeException(nameof(value.Value));
                if (value.Value > TimeSpan.FromHours(1440))
                    throw new ArgumentOutOfRangeException(nameof(value.Value));
#endif
            }

            _frequency = value;
        }
    }

    /// <summary>
    /// Тип СМС для отправки
    /// </summary>
    public SmsType SmsType { get; set; } = SmsType.Default;
    /// <summary>
    /// Бинарное сообщение передается вместе с UDH заголовком в начале в параметре Message, 
    /// в котором первый байт задает длину заголовка. Чтобы передать бинарное сообщение без UDH заголовка, 
    /// укажите нулевой байт в начале сообщения (00 в hex).<br/>
    /// Для возможности передачи параметров pid и dcs необходимо в конец бинарного сообщения добавить специальную 
    /// комбинацию "\n~~~\n" (перевод строки, 3 символа тильды и снова перевод строки) и затем текст "pid: значение1, 
    /// dcs: значение2" с точным сохранением пробелов.
    /// </summary>
    public BinaryMessage UseBinary { get; set; } = BinaryMessage.Default;

    /// <summary>
    /// При указании данного параметра, система не будет отображать текст сообщения, отправленного пользователю и 
    /// выводить предупреждение о необходимости подтверждения номера телефона, если с момента последнего подтверждения 
    /// прошло больше <see cref="SmsRequire"/> дней. Диапазон значений от 10 до 999.
    /// </summary>
    public int? SmsRequire
    {
        get => _smsRequire;
        set
        {
            if (value is not null)
            {
#if NET_CORE_APP_8
                ArgumentOutOfRangeException.ThrowIfLessThan(value.Value, 10);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Value, 999);
#else
                if (value.Value < 10)
                    throw new ArgumentOutOfRangeException(nameof(value.Value));
                if (value.Value > 999)
                    throw new ArgumentOutOfRangeException(nameof(value.Value));
#endif
            }

            _smsRequire = value;
        }
    }
    /// <summary>
    /// Полный http-адрес файла для загрузки и передачи в сообщении. Минимальный размер файла составляет 101 байт.
    /// </summary>
    public string? FileUrl { get; set; }
    /// <summary>
    /// Имя бота (telegram), в который необходимо отправить сообщение в формате "@botname_bot".
    /// </summary>
    public string? BotName { get; set; }
    /// <summary>
    /// Список параметров для голосового сообщения. Используется только при <see cref="SmsType"/> = <see cref="SmsType.Call"/>
    /// </summary>
    public CallConfiguration CallConfiguration { get; set; } = new CallConfiguration();

    /// <summary>
    /// Тема MMS или e-mail сообщения. При отправке e-mail указание темы, текста и адреса отправителя обязательно. 
    /// Для MMS обязательным является указание темы или текста. Если не указать тему MMS, 
    /// то в ее качестве будет использовано имя отправителя, переданное в запросе или используемое по умолчанию.
    /// </summary>
    public string? Subject { get; set; }
    /// <summary>
    /// Кодировка переданного сообщения.<br/>
    /// По умолчанию <see cref="Charset.WIN1251"/>
    /// </summary>
    public Charset Charset { get; set; } = Charset.UTF8;

    /// <summary>
    /// Признак необходимости получения стоимости рассылки.<br/>
    /// По умолчанию <see cref="Cost.Default"/>
    /// </summary>
    public Cost GetCost { get; set; } = Cost.Default;
    /// <summary>
    /// Формат ответа сервера об успешной отправке.<br/>
    /// По умолчанию <see cref="ResponseFormat.Default"/>
    /// </summary>
    public ResponseFormat ResponseFormat { get; set; } = ResponseFormat.Json;

    /// <summary>
    /// Срок "жизни" SMS-сообщения. Определяет время, в течение которого оператор будет пытаться доставить сообщение абоненту. 
    /// Диапазон от 1 до 24 часов.<br/>
    /// Значение по умолчанию 24 часа.
    /// </summary>
    public TimeSpan? Valid
    {
        get => _valid;
        set
        {
            if (value is not null)
            {
#if NET_CORE_APP_8
                ArgumentOutOfRangeException.ThrowIfLessThan(value.Value, TimeSpan.FromMinutes(1));
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Value, TimeSpan.FromDays(1));
#else
                if (value.Value < TimeSpan.FromMinutes(1))
                    throw new ArgumentOutOfRangeException(nameof(value.Value));
                if (value.Value > TimeSpan.FromDays(1))
                    throw new ArgumentOutOfRangeException(nameof(value.Value));
#endif
            }

            _valid = value;
        }
    }
    /// <summary>
    /// Максимальное количество SMS, на которые может разбиться длинное сообщение. 
    /// Слишком длинные сообщения будут обрезаться так, чтобы не переполнить количество SMS, требуемых для их передачи. 
    /// Этим параметром вы можете ограничить максимальную стоимость сообщений, так как за каждое SMS снимается отдельная плата.
    /// </summary>
    public int? MaxSms { get; set; }

    /// <summary>
    /// Значение буквенно-цифрового кода, введенного с "captcha" при использовании 
    /// <seealso href="https://www.smsc.ru/api/http/miscellaneous/antispam/">антиспам проверки</seealso>. 
    /// Данный параметр должен использоваться совместно с параметром <see cref="UserIp"/>.
    /// </summary>
    public string? ImageCode { get; set; }
    /// <summary>
    /// Значение IP-адреса, для которого будет действовать лимит на максимальное количество сообщений 
    /// с одного IP-адреса в сутки, установленный в <seealso href="https://www.smsc.ru/edit/">настройках</seealso> 
    /// личного кабинета в пункте "Лимиты и ограничения".
    /// </summary>
    public IPAddress? UserIp { get; set; }

    /// <summary>
    /// Признак необходимости добавления в ответ сервера списка ошибочных номеров.<br/>
    /// <see langword="false"/> - (по умолчанию) – не добавлять список (обычный ответ сервера).<br/>
    /// <see langword="true"/> - в ответ добавляется список ошибочных номеров телефонов с соответствующими статусами.
    /// </summary>
    public bool ErrorPhonesResponse { get; set; }
    /// <summary>
    /// Признак необходимости добавления в ответ сервера информации по каждому номеру.<br/>
    /// <see langword="false"/> - (по умолчанию) – не добавлять список (обычный ответ сервера).<br/>
    /// <see langword="true"/> - в ответ добавляется список всех номеров телефонов с соответствующими статусами, значениями mcc и mnc, 
    /// стоимостью, и, в случае ошибочных номеров, кодами ошибок.
    /// </summary>
    public bool FullErrorPhonesResponse { get; set; }

    /// <summary>
    /// Осуществляет привязку Клиента в качестве реферала к определенному ID партнера для текущего запроса.<br/>
    /// При передаче данного параметра Клиент с логином <see cref="ProviderConfiguration.Login"/> временно становится рефералом 
    /// партнера с ID <see cref="ReferalId"/>. Отчисления по партнерской программе будут сделаны именно для текущего запроса, 
    /// постоянной привязки не происходит. Данный параметр позволяет временно устанавливать Клиента в качестве реферала 
    /// из своих сервисов и программ, где нет возможности зарегистрировать Клиента по реферальной ссылке.
    /// </summary>
    public string? ReferalId { get; set; }

    /// <summary>
    /// Список файлов для добавления к запросу.
    /// </summary>
#if NET_FRAMEWORK
    public string? Files { get; set; }
#else
    public MultipartFormDataContent? Files { get; set; }
#endif
}
