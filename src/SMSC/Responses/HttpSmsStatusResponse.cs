#if NET_FRAMEWORK_AND_NET_STANDARD
using Newtonsoft.Json;
#endif

namespace SMSC.Responses;

/// <summary>
/// Структура ответа для запроса статуса сообщения
/// </summary>
public class HttpSmsStatusResponse
{
    /// <summary>
    /// код статуса (<seealso href="https://smsc.ru/api/http/status_messages/statuses/">список</seealso>)
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("status"), JsonConverter(typeof(StringToStatusJsonConverter))]
#else
    [JsonProperty(PropertyName = "status"), JsonConverter(typeof(StringToStatusJsonConverter))]
#endif
    public IStatusCode? Status { get; set; }
    /// <summary>
    /// Дата последнего изменения статуса. Формат DD.MM.YYYY hh:mm:ss.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("last_date"), JsonConverter(typeof(StringToDateTimeJsonConverter))]
#else
    [JsonProperty(PropertyName = "last_date"), JsonConverter(typeof(StringToDateTimeJsonConverter))]
#endif
    public DateTime? LastDate { get; set; }
    /// <summary>
    /// Штамп времени последнего изменения статуса.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("last_timestamp"), JsonConverter(typeof(StringToDateTimeJsonConverter))]
#else
    [JsonProperty(PropertyName = "last_timestamp"), JsonConverter(typeof(StringToDateTimeJsonConverter))]
#endif
    public DateTime? LastTimestamp { get; set; }
    /// <summary>
    /// Флаг в виде 2-х байтового числа, содержащий различную информацию о сообщении. Возможны комбинации значений битов разных характеристик. Биты 0-3 (тип сообщения): 0 – SMS, 1 – Flash-SMS, 2 – Бинарное SMS, 3 – Wap-push, 4 – HLR-запрос, 5 – Ping-SMS, 6 – MMS, 7 – Звонок, 8 – E-mail, 10 – Viber, 12 – Соцсети.<br/>
    /// Бит 5 – оплата сообщения со второго баланса.Бит 8 – признак шаблонного сообщения.<br/>
    /// Биты 10,9 – тип шаблонного сообщения(00 - сервисное, 01 - транзакционное, 10 - авторизационное, 11 - рекламное).
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("flag")]
#else
    [JsonProperty(PropertyName = "flag")]
#endif
    public short? Flag { get; set; }
    /// <summary>
    /// Код ошибки, если сообщение не было доставлено.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("err"), JsonConverter(typeof(StringToErrorCodeJsonConverter))]
#else
    [JsonProperty(PropertyName = "err"), JsonConverter(typeof(StringToErrorCodeJsonConverter))]
#endif
    public ISmsError? ErrorCode { get; set; }
    /// <summary>
    /// Дата отправки сообщения (формат DD.MM.YYYY hh:mm:ss).
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("send_date"), JsonConverter(typeof(StringToDateTimeJsonConverter))]
#else
    [JsonProperty(PropertyName = "send_date"), JsonConverter(typeof(StringToDateTimeJsonConverter))]
#endif
    public DateTime? SendDate { get; set; }
    /// <summary>
    /// Штамп времени отправки сообщения.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("send_timestamp"), JsonConverter(typeof(StringToDateTimeJsonConverter))]
#else
    [JsonProperty(PropertyName = "send_timestamp"), JsonConverter(typeof(StringToDateTimeJsonConverter))]
#endif
    public DateTime? SendTimestamp { get; set; }
    /// <summary>
    /// Номер телефона абонента или e-mail адрес.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("phone")]
#else
    [JsonProperty(PropertyName = "phone")]
#endif
    public string? Phone { get; set; }
    /// <summary>
    /// Название страны регистрации номера абонента.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("country")]
#else
    [JsonProperty(PropertyName = "country")]
#endif
    public string? Country { get; set; }
    /// <summary>
    /// Текущий сотовый оператор абонента.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("operator")]
#else
    [JsonProperty(PropertyName = "operator")]
#endif
    public string? Operator { get; set; }
    /// <summary>
    /// Оператор абонента по мобильному коду до портирования номера.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("operator_orig")]
#else
    [JsonProperty(PropertyName = "operator_orig")]
#endif
    public string? OriginalOperator { get; set; }
    /// <summary>
    /// Регион регистрации номера абонента.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("region")]
#else
    [JsonProperty(PropertyName = "region")]
#endif
    public string? Region { get; set; }
    /// <summary>
    /// Стоимость сообщения.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("cost")]
#else
    [JsonProperty(PropertyName = "cost")]
#endif
    public float? Cost { get; set; }
    /// <summary>
    /// Имя отправителя.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("sender_id")]
#else
    [JsonProperty(PropertyName = "sender_id")]
#endif
    public string? Sender { get; set; }
    /// <summary>
    /// Название статуса.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("status_name")]
#else
    [JsonProperty(PropertyName = "status_name")]
#endif
    public string? StatusName { get; set; }
    /// <summary>
    /// Текст сообщения.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("message")]
#else
    [JsonProperty(PropertyName = "message")]
#endif
    public string? Message { get; set; }
    /// <summary>
    /// Комментарий сообщения.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("comment")]
#else
    [JsonProperty(PropertyName = "comment")]
#endif
    public string? Comment { get; set; }
    /// <summary>
    /// Числовой код страны абонента плюс числовой код оператора абонента.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("mccmnc")]
#else
    [JsonProperty(PropertyName = "mccmnc")]
#endif
    public string? MccMnc { get; set; }
    /// <summary>
    /// Тип сообщения (0 – SMS, 1 – Flash-SMS, 2 – Бинарное SMS, 3 – Wap-push, 4 – HLR-запрос, 
    /// 5 – Ping-SMS, 6 – MMS, 7 – Звонок, 8 – E-mail, 10 – Viber, 12 – Соцсети).
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("type"), JsonConverter(typeof(IntToSmsTypeJsonConverter))]
#else
    [JsonProperty(PropertyName = "type"), JsonConverter(typeof(IntToSmsTypeJsonConverter))]
#endif
    public SmsType? MessageType { get; set; }
    /// <summary>
    /// Количество частей в SMS-сообщении (либо секунд в голосовом сообщении).
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("sms_cnt"), JsonConverter(typeof(NumberToStringJsonConverter))]
#else
    [JsonProperty(PropertyName = "sms_cnt"), JsonConverter(typeof(NumberToStringJsonConverter))]
#endif
    public string? SmsCountOrCallSeconds { get; set; }

    // Для HLR-сообщений
    /// <summary>
    /// уникальный код IMSI SIM-карты абонента.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("imsi")]
#else
    [JsonProperty(PropertyName = "imsi")]
#endif
    public string? IMSI { get; set; }
    /// <summary>
    /// Номер сервис-центра оператора, в сети которого находится абонент.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("msc")]
#else
    [JsonProperty(PropertyName = "msc")]
#endif
    public string? MSC { get; set; }
    /// <summary>
    /// Числовой код страны абонента.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("mcc")]
#else
    [JsonProperty(PropertyName = "mcc")]
#endif
    public int? MCC { get; set; }
    /// <summary>
    /// Числовой код оператора абонента.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("mnc")]
#else
    [JsonProperty(PropertyName = "mnc")]
#endif
    public int? MNC { get; set; }
    /// <summary>
    /// Название страны регистрации абонента.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("cn")]
#else
    [JsonProperty(PropertyName = "cn")]
#endif
    public string? HLRCountryName { get; set; }
    /// <summary>
    /// Название оператора регистрации абонента.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("net")]
#else
    [JsonProperty(PropertyName = "net")]
#endif
    public string? HLROperator { get; set; }
    /// <summary>
    /// Название роуминговой страны абонента при нахождении в чужой сети.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("rcn")]
#else
    [JsonProperty(PropertyName = "rcn")]
#endif
    public string? RoumingCountryName { get; set; }
    /// <summary>
    /// Название роумингового оператора абонента при нахождении в чужой сети.
    /// </summary>
#if NET_CORE_APP
    [JsonPropertyName("rnet")]
#else
    [JsonProperty(PropertyName = "rnet")]
#endif
    public string? RoumingOperatorName { get; set; }
}