using System.Web;

namespace SMSC.Http;

public class HttpSms(
	ProviderConfiguration configuration,
	HttpClient? httpClient = null) : HttpAbstract(configuration, httpClient)
{
	private SmsConfiguration? _smsConfiguration => SmsConfiguration as SmsConfiguration;

	/// <summary>
	/// Название endpoint для отправки команд
	/// </summary>
	protected override string SiteAddressCommand { get; } = "send.php";

	/// <inheritdoc/>
	protected override bool SupportsJsonRequestFormat => true;

	/// <summary>
	/// Отправка СМС клиенту на номер <paramref name="client"/> с текстом <paramref name="message"/>
	/// </summary>
	/// <exception cref="MessageMaxLengthException"/>
	public virtual async Task<HttpSmsResponse> SendSms(string? client, string? message, SmsConfiguration config, CancellationToken cancellationToken = default)
	{
		ThrowHelper.ThrowIfNullOrEmpty(client, nameof(client));
		EnsureMessageLength(message);

		SmsConfiguration = config;
		return await (await SendRequest(CreateRequest((@params) =>
		{
			@params.Add("phones", client);
			if (config.SmsType != SmsType.HLR & config.SmsType != SmsType.Ping)
			{
				if (message is not null)
					@params.Add("mes", message);
			}
		}), cancellationToken))
			.CreateHttpSmsResponseAsync(config);
	}

	/// <summary>
	/// Отправка одного и того же СМС списку клиентов <paramref name="clients"/> с текстом <paramref name="message"/>
	/// </summary>
	/// <exception cref="MessageMaxLengthException"/>
	/// <exception cref="ArgumentOutOfRangeException"/>
	public virtual async Task<HttpSmsResponse> SendSms(IEnumerable<string?> clients, string? message, SmsConfiguration config, CancellationToken cancellationToken = default)
	{
		if (!clients.Any())
			throw new ArgumentOutOfRangeException(nameof(clients), "Clients can`t be empty. Try fill it.");

		EnsureMessageLength(message);
		SmsConfiguration = config;

		var phones = string.Join(PhonesDelimeter, clients.Where(c => !string.IsNullOrEmpty(c)).Select(c => c!.ToString()));
		return await (await SendRequest(CreateRequest((@params) =>
		{
			@params.Add("phones", phones);
			if (config.SmsType != SmsType.HLR & config.SmsType != SmsType.Ping)
			{
				if (message is not null)
					@params.Add("mes", message);
			}
		}), cancellationToken))
			.CreateHttpSmsResponseAsync(config);
	}

	/// <summary>
	/// Отправка списка номеров телефонов и соответствующих им сообщений
	/// </summary>
	/// <exception cref="MessageMaxLengthException"/>
	public virtual async Task<HttpSmsResponse> SendSms(IEnumerable<SendItem> sendItems, SmsConfiguration config, CancellationToken cancellationToken = default)
	{
		if (!sendItems.Any())
			throw new ArgumentOutOfRangeException(nameof(sendItems), "Send items can`t be empty. Try fill it.");

		foreach (var item in sendItems)
			EnsureMessageLength(item.Message);

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
	/// <exception cref="MessageMaxLengthException"/>
	public virtual async Task<HttpSmsResponse> SendSms(IEnumerable<SendItemComplicated> sendItems, SmsConfiguration config, CancellationToken cancellationToken = default)
	{
		if (!sendItems.Any())
			throw new ArgumentOutOfRangeException(nameof(sendItems), "Send items can`t be empty. Try fill it.");

		foreach (var item in sendItems)
			EnsureMessageLength(item.Message);

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
	/// <exception cref="MessageMaxLengthException"/>
	/// <exception cref="InvalidOperationException"/>
	public virtual async Task<HttpSmsResponse> SendGroupSms(string groupId, string? message, SmsConfiguration config, CancellationToken cancellationToken = default)
	{
		ThrowHelper.ThrowIfNullOrEmpty(groupId, nameof(groupId));
		EnsureMessageLength(message);

		if (groupId.Contains(",") || groupId.Contains(";"))
			throw new InvalidOperationException($"{nameof(groupId)} can`t contains , or ;");

		SmsConfiguration = config;

		var group = groupId.Trim();
		if (group.ToLower().StartsWith("g"))
			group = group.ToLower().TrimStart('g');

		return await (await SendRequest(CreateRequest((@params) =>
		{
			@params.Add("phones", "G" + group);
			if (config.SmsType != SmsType.HLR & config.SmsType != SmsType.Ping)
			{
				if (message is not null)
					@params.Add("mes", message);
			}
		}), cancellationToken))
			.CreateHttpSmsResponseAsync(config);
	}

	/// <inheritdoc/>
	protected override Dictionary<string, string> GetQueryDictionary()
	{
		CheckSmsConfiguration();

		var @params = new Dictionary<string, string>();
		AddSmsType(@params);

		if (_smsConfiguration!.Id is not null)
			@params.Add("id", _smsConfiguration.Id);
		if (_smsConfiguration!.Sender is not null)
		{
			ThrowHelper.ThrowIfGreaterThan(_smsConfiguration!.Sender.Length, 15);
			@params.Add("sender", _smsConfiguration.Sender);
		}
		if (_smsConfiguration.Transliteration != Transliteration.Default)
		{
			switch (_smsConfiguration.Transliteration)
			{
				case Transliteration.Translit: @params.Add("translit", "1"); break;
				case Transliteration.TranslitExtend: @params.Add("translit", "2"); break;
				default: throw new InvalidOperationException($"{_smsConfiguration.Transliteration}");
			}
		}
		if (_smsConfiguration!.TinyUrl)
			@params.Add("tinyurl", "1");
		if (!string.IsNullOrEmpty(_smsConfiguration!.TimeToSend))
		{
			if (!VerifyTimeToSendParameter())
				throw new HttpSmsParametersException($"{nameof(_smsConfiguration.TimeToSend)} format is not valid.");

			switch (_smsConfiguration.Time)
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
			ThrowHelper.ThrowIfNullOrEmpty(_smsConfiguration.Subject);
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

	/// <inheritdoc/>
	protected override HttpMethod GetRequestMethod(Uri requestUri, Dictionary<string, string> payload)
	{
		CheckSmsConfiguration();

		if (_smsConfiguration!.Files is not null)
			return HttpMethod.Post;
		if (_smsConfiguration.SmsType is SmsType.Mail or SmsType.Call)
			return HttpMethod.Post;
		if (requestUri.OriginalString.Length > MaxQueryStringLength)
			return HttpMethod.Post;

		return HttpMethod.Get;
	}

	/// <inheritdoc/>
	protected override bool ShouldPostParametersInBody(Dictionary<string, string> payload)
	{
		CheckSmsConfiguration();
		if (_smsConfiguration!.Files is not null)
			return false;
		return _smsConfiguration.SmsType is SmsType.Mail or SmsType.Call
			|| CreateQueryString(payload).OriginalString.Length > MaxQueryStringLength;
	}

	/// <inheritdoc/>
	protected override HttpRequestMessage CreateRequestMessage(Uri requestUri, Dictionary<string, string> payload)
	{
		if (!UseJsonRequestFormat
			&& _smsConfiguration!.SmsType == SmsType.MMS
			&& _smsConfiguration.FileUrl is null
			&& _smsConfiguration.Files is not null)
		{
			return new HttpRequestMessage(HttpMethod.Post, requestUri)
			{
				Content = _smsConfiguration.Files
			};
		}

		return base.CreateRequestMessage(requestUri, payload);
	}

	private static void AddNewLineParameter(Dictionary<string, string> @params)
		=> @params.Add("nl", "1");

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
			case SmsType.WhatsApp:
				ThrowHelper.ThrowIfNullOrEmpty(_smsConfiguration.BotName, nameof(_smsConfiguration.BotName));
				if (!_smsConfiguration.BotName.StartsWith("wa:", StringComparison.OrdinalIgnoreCase))
					throw new ArgumentException("WhatsApp BotName must be in format wa:botnumber.", nameof(_smsConfiguration.BotName));
				return;
			case SmsType.Telegram: key = "tg"; val = "1"; break;
			case SmsType.Call: key = "call"; val = "1"; break;
			default: throw new NotImplementedException($"Type {_smsConfiguration.SmsType} is unknown.");
		}

		if (!string.IsNullOrEmpty(key) & !string.IsNullOrEmpty(val))
			@params.Add(key, val);
	}

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
