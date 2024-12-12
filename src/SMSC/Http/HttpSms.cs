using System.Web;

namespace SMSC.Http;

public class HttpSms(
    ProviderConfiguration configuration) : HttpAbstract(configuration)
{
    private SmsConfiguration? _smsConfiguration => SmsConfiguration as SmsConfiguration;

    /// <summary>
    /// Название endpoint для отправки команд
    /// </summary>
    protected override string SiteAddressCommand { get; } = "send.php";

    /// <summary>
    /// Отправка СМС клиенту на номер <paramref name="client"/> с текстом <paramref name="message"/>
    /// </summary>
    /// <param name="client">Номер мобильного телефона в международном формате, на который отправляется сообщение. 
    /// Номер могут передаваться без знака "+". Если номер передан без знака "+", то он может быть исправлен автоматическим 
    /// форматированием и приведен к правильному международному формату. Таким образом, некоторые ошибки при вводе номеров 
    /// телефонов могут быть исправлены автоматически. Для отключения автоисправления передайте номер со знаком "+".<br/>
    /// Для <b>e-mail</b> сообщения передается список e-mail адресов получателей.<br/>
    /// Для <b>telegram</b> в качестве получателя сообщения возможно указание ника абонента или его ID в виде #ID.</param>
    /// <param name="message">Текст отправляемого сообщения. Максимальный размер – 1000 символов. Сообщение при необходимости 
    /// будет разбито на несколько SMS, отправленных абоненту и оплаченных по отдельности. 
    /// Размер одного SMS – 160 символов в латинице или 70 символов в кириллице. При разбивке сообщения на несколько SMS в 
    /// каждую часть добавляется заголовок для объединения частей в одно сообщение на телефоне получателя, 
    /// и максимальная длина становится 67 для кириллицы и 153 для латинских букв. В текст сообщения можно добавлять <seealso href="https://www.smsc.ru/api/http/send/smsinfo/">комментарии</seealso>, 
    /// предназначенные для просмотра отправителем истории в личном кабинете.</param>
    /// <exception cref="MessageMaxLengthException"/>
    public virtual async Task<HttpSmsResponse> SendSms(StringValues client, StringValues message, SmsConfiguration config, CancellationToken cancellationToken = default)
    {
        SmsConfiguration = config;
        return await (await SendRequest(CreateRequest((@params) =>
        {
            @params.Add("phones", client.ToString());
            if (config.SmsType != SmsType.HLR & config.SmsType != SmsType.Ping)
                @params.Add("mes", message.ToString());
        }), cancellationToken))
            .CreateHttpSmsResponseAsync(config);
    }
    /// <summary>
    /// Отправка одного и того же СМС списку клиентов <paramref name="clients"/> с текстом <paramref name="message"/>
    /// </summary>
    /// <param name="client">Номер мобильного телефона в международном формате, на который отправляется сообщение. 
    /// Номер могут передаваться без знака "+". Если номер передан без знака "+", то он может быть исправлен автоматическим 
    /// форматированием и приведен к правильному международному формату. Таким образом, некоторые ошибки при вводе номеров 
    /// телефонов могут быть исправлены автоматически. Для отключения автоисправления передайте номер со знаком "+".<br/>
    /// Для <b>e-mail</b> сообщения передается список e-mail адресов получателей.<br/>
    /// Для <b>telegram</b> в качестве получателя сообщения возможно указание ника абонента или его ID в виде #ID.</param>
    /// <param name="message">Текст отправляемого сообщения. Максимальный размер – 1000 символов. Сообщение при необходимости 
    /// будет разбито на несколько SMS, отправленных абоненту и оплаченных по отдельности. 
    /// Размер одного SMS – 160 символов в латинице или 70 символов в кириллице. При разбивке сообщения на несколько SMS в 
    /// каждую часть добавляется заголовок для объединения частей в одно сообщение на телефоне получателя, 
    /// и максимальная длина становится 67 для кириллицы и 153 для латинских букв. В текст сообщения можно добавлять <seealso href="https://www.smsc.ru/api/http/send/smsinfo/">комментарии</seealso>, 
    /// предназначенные для просмотра отправителем истории в личном кабинете.</param>
    /// <exception cref="MessageMaxLengthException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public virtual async Task<HttpSmsResponse> SendSms(IEnumerable<StringValues> clients, StringValues message, SmsConfiguration config, CancellationToken cancellationToken = default)
    {
        if (!clients.Any())
            throw new ArgumentOutOfRangeException(nameof(clients), "Clients can`t be empty. Try fill it.");

        SmsConfiguration = config;

        var phones = string.Join(PhonesDelimeter, clients.Select(c => c.ToString()));
        return await (await SendRequest(CreateRequest((@params) =>
        {
            @params.Add("phones", phones);
            if (config.SmsType != SmsType.HLR & config.SmsType != SmsType.Ping)
                @params.Add("mes", message.ToString());
        }), cancellationToken))
            .CreateHttpSmsResponseAsync(config);
    }

    /// <summary>
    /// Отправка списка номеров телефонов и соответствующих им сообщений
    /// </summary>
    /// <param name="sendItems">Список номеров телефонов и соответствующих им сообщений</param>
    /// <exception cref="MessageMaxLengthException"/>
    public virtual async Task<HttpSmsResponse> SendSms(IEnumerable<SendItem> sendItems, SmsConfiguration config, CancellationToken cancellationToken = default)
    {
        if (!sendItems.Any())
            throw new ArgumentOutOfRangeException(nameof(sendItems), "Send items can`t be empty. Try fill it.");

        SmsConfiguration = config;

        var list = string.Join("\n", sendItems.Select(s => $"{s.Phone}:{s.Message}"));
        return await (await SendRequest(CreateRequest((@params) =>
        {
            @params.Add("list", list);
            if (sendItems.Any(si => si.UseNewLineMode))
                AddNewLineParameter(@params);
        }), cancellationToken))
            .CreateHttpSmsResponseAsync(config);
    }
    /// <summary>
    /// В случае необходимости передачи разных имен отправителей (и, возможно, различных часовых поясов абонентов 
    /// (работает только для запросов, в которых параметр <see cref="SmsConfiguration.Time"/> представлен в виде <see cref="TimeFormat.Full"/>)) 
    /// для разных сообщений
    /// </summary>
    /// <param name="sendItems">Список номеров телефонов и соответствующих им сообщений</param>
    /// <exception cref="MessageMaxLengthException"/>
    public virtual async Task<HttpSmsResponse> SendSms(IEnumerable<SendItemComplicated> sendItems, SmsConfiguration config, CancellationToken cancellationToken = default)
    {
        if (!sendItems.Any())
            throw new ArgumentOutOfRangeException(nameof(sendItems), "Send items can`t be empty. Try fill it.");

        SmsConfiguration = config;

        var list = string.Join("\n", sendItems.Select(s => $"{s.Sender}{(s.TimeZone is not null ? "," + s.TimeZone : string.Empty)}|{s.Phone}:{s.Message}"));
        return await (await SendRequest(CreateRequest((@params) =>
        {
            @params.Add("list", list);
            if (sendItems.Any(si => si.UseNewLineMode))
                AddNewLineParameter(@params);
        }), cancellationToken))
            .CreateHttpSmsResponseAsync(config);
    }
    /// <summary>
    /// Отправка сообщения <see href="https://www.smsc.ru/api/http/send/group/">группе номеров</see>.
    /// </summary>
    /// <param name="groupId">Идентификатор группы номеров. Отредактировать можно <see href="https://www.smsc.ru/groups/edit/">здесь</see>.</param>
    /// <param name="message">Текст отправляемого сообщения. Максимальный размер – 1000 символов. Сообщение при необходимости 
    /// будет разбито на несколько SMS, отправленных абоненту и оплаченных по отдельности. 
    /// Размер одного SMS – 160 символов в латинице или 70 символов в кириллице. При разбивке сообщения на несколько SMS в 
    /// каждую часть добавляется заголовок для объединения частей в одно сообщение на телефоне получателя, 
    /// и максимальная длина становится 67 для кириллицы и 153 для латинских букв. В текст сообщения можно добавлять <seealso href="https://www.smsc.ru/api/http/send/smsinfo/">комментарии</seealso>, 
    /// предназначенные для просмотра отправителем истории в личном кабинете.</param>
    /// <exception cref="MessageMaxLengthException"/>
    /// <exception cref="InvalidOperationException"/>
    public virtual async Task<HttpSmsResponse> SendGroupSms(StringValues groupId, StringValues message, SmsConfiguration config, CancellationToken cancellationToken = default)
    {
        if (groupId.Contains(",") || groupId.Contains(";"))
            throw new InvalidOperationException($"{nameof(groupId)} can`t contains , or ;");

        SmsConfiguration = config;

        var group = groupId.ToString().Trim();
        if (group.ToLower().StartsWith("g"))
            group = group.ToLower().TrimStart('g');

        return await (await SendRequest(CreateRequest((@params) =>
        {
            @params.Add("phones", "G" + group);
            if (config.SmsType != SmsType.HLR & config.SmsType != SmsType.Ping)
                @params.Add("mes", message.ToString());
        }), cancellationToken))
            .CreateHttpSmsResponseAsync(config);
    }

    /// <summary>
    /// Настройка строки запроса
    /// </summary>
    /// <param name="config">Конфигурация для отправки запроса</param>
    /// <returns>Список параметров в виде словаря (ключ-значение)</returns>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="InvalidOperationException"/>
    protected override Dictionary<string, string> GetQueryDictionary()
    {
        CheckSmsConfiguration();

        var @params = new Dictionary<string, string>();
        AddSmsType(@params);

        if (_smsConfiguration!.Id is not null)
            @params.Add("id", _smsConfiguration.Id);
        if (_smsConfiguration!.Sender is not null)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(_smsConfiguration!.Sender.Length, 15);
            @params.Add("sender", _smsConfiguration.Sender);
        }
        if (_smsConfiguration.Transliteration != Transliteration.Default)
        {
            switch (_smsConfiguration.Transliteration)
            {
                case Transliteration.Translit: @params.Add("translit", "1"); break;
                case Transliteration.TranslitExtend: @params.Add("translit", "2"); break;
                default: throw new InvalidOperationException($"{Enum.GetName(_smsConfiguration.Transliteration)}");
            }
        }
        if (_smsConfiguration!.TinyUrl)
            @params.Add("tinyurl", "1");
        if (!string.IsNullOrEmpty(_smsConfiguration!.TimeToSend))
        {
            if (!VerifyTimeToSendParameter())
                throw new HttpSmsParametersException($"{nameof(_smsConfiguration.TimeToSend)} format is not valid.");

            switch(_smsConfiguration.Time)
            {
                case TimeFormat.TimeStamp:
                    @params.Add("time", "0" + _smsConfiguration!.TimeToSend);
                    break;
                case TimeFormat.TimeShift:
                    @params.Add("time", HttpUtility.UrlEncode("+") + _smsConfiguration!.TimeToSend);
                    break;
                default: 
                    @params.Add("time", _smsConfiguration!.TimeToSend);
                    break;
            }
            
        }
        if (_smsConfiguration!.TimeZone is not null)
            @params.Add("tz", _smsConfiguration!.TimeZone.Value.ToString());
        if (_smsConfiguration!.Period is not null)
        {
            if (!_smsConfiguration.Frequency.HasValue)
                throw new ArgumentNullException(nameof(_smsConfiguration.Frequency));

            @params.Add("period", _smsConfiguration!.Period.Value.TotalHours.ToString("0.#"));
            @params.Add("freq", _smsConfiguration!.Frequency.Value.TotalMinutes.ToString("0"));
        }
        if (_smsConfiguration!.BotName is not null)
            @params.Add("bot", _smsConfiguration.BotName);
        if (_smsConfiguration!.SmsRequire is not null)
            @params.Add("smsreq", _smsConfiguration!.SmsRequire.Value.ToString());
        if (_smsConfiguration!.FileUrl is not null)
            @params.Add("fileurl", _smsConfiguration.FileUrl);
        if (_smsConfiguration.SmsType == SmsType.Call)
        {
            var voice = _smsConfiguration.CallConfiguration.Voice switch
            {
                Voice.Male2 => "m2",
                Voice.Male3 => "m3",
                Voice.Male4 => "m4",
                Voice.Female1 => "w",
                Voice.Female2 => "w2",
                Voice.Female3 => "w3",
                Voice.Female4 => "w4",
                _ => "m",
            };
            @params.Add("voice", _smsConfiguration.CallConfiguration.VoiceLanguage is null ?
                voice : $"{voice},{_smsConfiguration.CallConfiguration.VoiceLanguage}");

            @params.Add("param", $"{_smsConfiguration.CallConfiguration.Wait},{_smsConfiguration.CallConfiguration.RepeatSpan}," +
                $"{_smsConfiguration.CallConfiguration.RetryCount}");
        }
        if (_smsConfiguration.SmsType == SmsType.Mail)
            ArgumentException.ThrowIfNullOrEmpty(_smsConfiguration.Subject);
        if (_smsConfiguration.Subject is not null)
            @params.Add("subj", _smsConfiguration.Subject);

        AddCharset(@params);

        if (_smsConfiguration.GetCost != Cost.Default)
        {
            switch (_smsConfiguration.GetCost)
            {
                case Cost.Cost: @params.Add("cost", "1"); break;
                case Cost.CostSend: @params.Add("cost", "2"); break;
                case Cost.CostAndBalance: @params.Add("cost", "3"); break;
            }
        }

        AddResponseFormat(@params);

        if (_smsConfiguration.Valid is not null)
            @params.Add("valid", $"{_smsConfiguration.Valid.Value.Hours:00}:{_smsConfiguration.Valid.Value.Minutes:00}");
        if (_smsConfiguration.MaxSms is not null)
            @params.Add("maxsms", _smsConfiguration.MaxSms.Value.ToString());
        if (_smsConfiguration.UserIp is not null)
        {
            @params.Add("userip", _smsConfiguration.UserIp.ToString());
            if (_smsConfiguration.ImageCode is not null)
                @params.Add("imgcode", _smsConfiguration.ImageCode);
        }
        if (_smsConfiguration.ErrorPhonesResponse)
            @params.Add("err", "1");
        if (_smsConfiguration.FullErrorPhonesResponse)
            @params.Add("op", "1");
        if (_smsConfiguration.ReferalId is not null)
            @params.Add("pp", _smsConfiguration.ReferalId);

        return @params;
    }
    /// <summary>
    /// При использовании одного из параметров (отправка email или прикрепление файла к сообщению)
    /// запрос должен отправляться методом POST
    /// </summary>
    /// <param name="config">Список параметров запроса</param>
    /// <returns>
    /// <see langword="true"/> - необходимо использовать метод POST<br/>
    /// <see langword="false"/> - необходимо использовать метод GET
    /// </returns>
    /// <exception cref="ArgumentNullException"/>
    protected override HttpMethod GetRequestMethod()
    {
        CheckSmsConfiguration();
        return _smsConfiguration!.Files is not null ? HttpMethod.Post : HttpMethod.Get;
    }
    /// <summary>
    /// Создание объекта <see cref="HttpRequestMessage"/> для отправки запроса
    /// </summary>
    /// <param name="requestUri">Строка запроса</param>
    /// <param name="params">Список параметров запроса</param>
    /// <returns>Объект класса <see cref="HttpRequestMessage"/></returns>
    protected override HttpRequestMessage CreateRequestMessage(Uri requestUri, Dictionary<string, string> @params)
    {
        var request = base.CreateRequestMessage(requestUri, @params);
        if (!UseJsonRequestFormat)
        {
            if (_smsConfiguration!.SmsType == SmsType.MMS && _smsConfiguration.FileUrl is null)
            {
                if (_smsConfiguration!.Files is not null)
                    request.Content = _smsConfiguration!.Files;
            }
        }

        return request;
    }

    /// <summary>
    /// Добавление параметра nl для мультистрокового соообщения Message
    /// </summary>
    /// <param name="params"></param>
    private static void AddNewLineParameter(Dictionary<string, string> @params)
        => @params.Add("nl", "1");
    /// <summary>
    /// Проверка типа СМС сообщения
    /// </summary>
    /// <param name="params">Словарь с параметрами СМС сообщения</param>
    /// <exception cref="NotImplementedException"/>
    private void AddSmsType(Dictionary<string, string> @params)
    {
        string? key, val;

        switch (_smsConfiguration!.SmsType)
        {
            case SmsType.Default: return;
            case SmsType.Flash: key = "flash"; val = "1"; break;
            case SmsType.Bin:
                {
                    key = "bin";
                    switch (_smsConfiguration.UseBinary)
                    {
                        case BinaryMessage.Http:
                            val = "1";
                            break;
                        case BinaryMessage.Hex:
                            val = "2";
                            break;
                        case BinaryMessage.Default:
                        default:
                            val = "0";
                            break;
                    }
                    break;
                }
            case SmsType.WapPush: key = "push"; val = "1"; break;
            case SmsType.HLR: key = "hlr"; val = "1"; break;
            case SmsType.Ping: key = "ping"; val = "1"; break;
            case SmsType.MMS: key = "mms"; val = "1"; break;
            case SmsType.Mail: key = "mail"; val = "1"; break;
            case SmsType.Socials: key = "soc"; val = "1"; break;
            case SmsType.Viber: key = "viber"; val = "1"; break;
            case SmsType.WhatsApp: key = "whatsapp"; val = "1"; break;
            case SmsType.Telegram: key = "tg"; val = "1"; break;
            case SmsType.Call: key = "call"; val = "1"; break;
            default: throw new NotImplementedException($"Type {Enum.GetName(_smsConfiguration.SmsType)} is unknown.");
        }

        if (!string.IsNullOrEmpty(key) & !string.IsNullOrEmpty(val))
            @params.Add(key, val);
    }
    /// <summary>
    /// Проверка на корректность задания параметра СМС сообщения - <see cref="SmsConfiguration.TimeToSend"/>
    /// в соответствии с параметром <see cref="SmsConfiguration.Time"/>
    /// </summary>
    /// <returns>
    /// <see langword="true"/> - если формат соответствует<br/>
    /// <see langword="false"/> - если не соответствует
    /// </returns>
    private bool VerifyTimeToSendParameter()
    {
        switch (_smsConfiguration!.Time)
        {
            case TimeFormat.Now: return true;
            case TimeFormat.Full:
                {
                    var regex = new Regex(@"\d{10}|(\d{2}\.\d{2}.\d{2}\s\d{2}:\d{2})");
                    return regex.IsMatch(_smsConfiguration.TimeToSend!);
                }
            case TimeFormat.TimeSpan:
                {
                    var regex = new Regex(@"\d{1,2}-\d{1,2}");
                    return regex.IsMatch(_smsConfiguration.TimeToSend!);
                }
            case TimeFormat.TimeStamp:
                {
                    try
                    {
                        long.Parse(_smsConfiguration!.TimeToSend!);
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }
            case TimeFormat.TimeShift:
                {
                    return int.TryParse(_smsConfiguration!.TimeToSend!, out _);
                }
            default: return false;
        }
    }
}