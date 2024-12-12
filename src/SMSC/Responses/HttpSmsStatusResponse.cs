namespace SMSC.Responses;

public class HttpSmsStatusResponse
{
    /// <summary>
    /// код статуса (<seealso href="https://smsc.ru/api/http/status_messages/statuses/">список</seealso>)
    /// </summary>
    [JsonPropertyName("status")]
    public int? Status { get; set; }
    /// <summary>
    /// Дата последнего изменения статуса. Формат DD.MM.YYYY hh:mm:ss.
    /// </summary>
    [JsonPropertyName("last_date"), JsonConverter(typeof(StringToDateTimeJsonConverter))]
    public DateTime? LastDate { get; set; }
    /// <summary>
    /// Штамп времени последнего изменения статуса.
    /// </summary>
    [JsonPropertyName("last_timestamp"), JsonConverter(typeof(StringToDateTimeJsonConverter))]
    public DateTime? LastTimestamp { get; set; }
    /// <summary>
    /// Флаг в виде 2-х байтового числа, содержащий различную информацию о сообщении. Возможны комбинации значений битов разных характеристик. Биты 0-3 (тип сообщения): 0 – SMS, 1 – Flash-SMS, 2 – Бинарное SMS, 3 – Wap-push, 4 – HLR-запрос, 5 – Ping-SMS, 6 – MMS, 7 – Звонок, 8 – E-mail, 10 – Viber, 12 – Соцсети.<br/>
    /// Бит 5 – оплата сообщения со второго баланса.Бит 8 – признак шаблонного сообщения.<br/>
    /// Биты 10,9 – тип шаблонного сообщения(00 - сервисное, 01 - транзакционное, 10 - авторизационное, 11 - рекламное).
    /// </summary>
    [JsonPropertyName("flag")]
    public short? Flag { get; set; }
    /// <summary>
    /// Код ошибки, если сообщение не было доставлено.
    /// </summary>
    [JsonPropertyName("err")]
    public int? ErrorCode { get; set; }
    /// <summary>
    /// Дата отправки сообщения (формат DD.MM.YYYY hh:mm:ss).
    /// </summary>
    [JsonPropertyName("send_date"), JsonConverter(typeof(StringToDateTimeJsonConverter))]
    public DateTime? SendDate { get; set; }
    /// <summary>
    /// Штамп времени отправки сообщения.
    /// </summary>
    [JsonPropertyName("send_timestamp"), JsonConverter(typeof(StringToDateTimeJsonConverter))]
    public DateTime? SendTimestamp { get; set; }
    /// <summary>
    /// Номер телефона абонента или e-mail адрес.
    /// </summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }
    /// <summary>
    /// Название страны регистрации номера абонента.
    /// </summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }
    /// <summary>
    /// Текущий сотовый оператор абонента.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }
    /// <summary>
    /// Оператор абонента по мобильному коду до портирования номера.
    /// </summary>
    [JsonPropertyName("operator_orig")]
    public string? OriginalOperator { get; set; }
    /// <summary>
    /// Регион регистрации номера абонента.
    /// </summary>
    [JsonPropertyName("region")]
    public string? Region { get; set; }
    /// <summary>
    /// Стоимость сообщения.
    /// </summary>
    [JsonPropertyName("cost")]
    public float? Cost { get; set; }
    /// <summary>
    /// Имя отправителя.
    /// </summary>
    [JsonPropertyName("sender_id")]
    public string? Sender { get; set; }
    /// <summary>
    /// Название статуса.
    /// </summary>
    [JsonPropertyName("status_name")]
    public string? StatusName { get; set; }
    /// <summary>
    /// Текст сообщения.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    /// <summary>
    /// Комментарий сообщения.
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }
    /// <summary>
    /// Числовой код страны абонента плюс числовой код оператора абонента.
    /// </summary>
    [JsonPropertyName("mccmnc")]
    public string? MccMnc { get; set; }
    /// <summary>
    /// Тип сообщения (0 – SMS, 1 – Flash-SMS, 2 – Бинарное SMS, 3 – Wap-push, 4 – HLR-запрос, 
    /// 5 – Ping-SMS, 6 – MMS, 7 – Звонок, 8 – E-mail, 10 – Viber, 12 – Соцсети).
    /// </summary>
    [JsonPropertyName("type")]
    public int? MessageType { get; set; }
    /// <summary>
    /// Количество частей в SMS-сообщении (либо секунд в голосовом сообщении).
    /// </summary>
    [JsonPropertyName("sms_cnt"), JsonConverter(typeof(NumberToStringJsonConverter))]
    public string? SmsCountOrCallSeconds { get; set; }

    // Для HLR-сообщений
    /// <summary>
    /// уникальный код IMSI SIM-карты абонента.
    /// </summary>
    [JsonPropertyName("imsi")]
    public string? IMSI { get; set; }
    /// <summary>
    /// Номер сервис-центра оператора, в сети которого находится абонент.
    /// </summary>
    [JsonPropertyName("msc")]
    public string? MSC { get; set; }
    /// <summary>
    /// Числовой код страны абонента.
    /// </summary>
    [JsonPropertyName("mcc")]
    public int? MCC { get; set; }
    /// <summary>
    /// Числовой код оператора абонента.
    /// </summary>
    [JsonPropertyName("mnc")]
    public int? MNC { get; set; }
    /// <summary>
    /// Название страны регистрации абонента.
    /// </summary>
    [JsonPropertyName("cn")]
    public string? HLRCountryName { get; set; }
    /// <summary>
    /// Название оператора регистрации абонента.
    /// </summary>
    [JsonPropertyName("net")]
    public string? HLROperator { get; set; }
    /// <summary>
    /// Название роуминговой страны абонента при нахождении в чужой сети.
    /// </summary>
    [JsonPropertyName("rcn")]
    public string? RoumingCountryName { get; set; }
    /// <summary>
    /// Название роумингового оператора абонента при нахождении в чужой сети.
    /// </summary>
    [JsonPropertyName("rnet")]
    public string? RoumingOperatorName { get; set; }
}