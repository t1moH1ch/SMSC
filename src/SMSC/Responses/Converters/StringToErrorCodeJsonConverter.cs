#if NET_FRAMEWORK_AND_NET_STANDARD
using Newtonsoft.Json;
#endif

namespace SMSC.Responses.Converters;

#if NET_CORE_APP
internal class StringToErrorCodeJsonConverter : JsonConverter<ISmsError>
{
    public override ISmsError? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        int code;

        if (reader.TokenType == JsonTokenType.String)
        {
            var propValue = reader.GetString() ?? string.Empty;
            if (!int.TryParse(propValue, out code))
                return null;
        }
        else if (reader.TokenType == JsonTokenType.Number)
            code = reader.GetInt32();
        else return null;

        return code switch
        {
            0 => new NoError(),
            1 => new SubscriberNotExistsError(),
            2 => new MailboxFullError(),
            3 => new RecipientServerNotFoundError(),
            4 => new MailboxBlockError(),
            6 => new SubscriberIsOfflineError(),
            11 => new ServiceNotConnectedError(),
            12 => new SubscriberPhoneError(),
            13 => new SubscriberBlockedError(),
            21 => new NoServiceSupportError(),
            200 => new VirtualSendingError(),
            219 => new SimCardReplacementError(),
            220 => new OperatorQueueFullError(),
            237 => new SubscriberNotRespondingError(),
            238 => new NoTemplateError(),
            239 => new ForbiddenIpAddressError(),
            240 => new SubscriberBusyError(),
            241 => new ConversionError(),
            242 => new AnsweringMachineError(),
            243 => new ContractConcludedError(),
            244 => new MailingProhibitedError(),
            245 => new StatusNotReceivedError(),
            246 => new TimeLimitError(),
            247 => new MessageLimitExceededError(),
            248 => new NoRouteError(),
            249 => new InvalidNumberFormatError(),
            250 => new SettingsNumberForbiddenError(),
            251 => new OneNumberLimitExceededError(),
            252 => new NumberForbiddenError(),
            253 => new SpamFilterProhibitedError(),
            254 => new UnregisteredSenderIdError(),
            255 => new OperatorRejectedError(),
            _ => throw new NotImplementedException($"Type with error code {code} not implemented."),
        };
    }

    public override void Write(Utf8JsonWriter writer, ISmsError value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Code);
    }
}
#else
internal class StringToErrorCodeJsonConverter : JsonConverter<ISmsError>
{
    public override ISmsError? ReadJson(JsonReader reader, Type objectType, ISmsError? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        int code;

        if (reader.TokenType == JsonToken.String)
        {
            var propValue = reader.ReadAsString() ?? string.Empty;
            if (!int.TryParse(propValue, out code))
                return null;
        }
        else if (reader.TokenType == JsonToken.Integer)
        {
            var codeValue = reader.ReadAsInt32();
            if (!codeValue.HasValue)
                return null;

            code = codeValue.Value;
        }
        else return null;

        return code switch
        {
            0 => new NoError(),
            1 => new SubscriberNotExistsError(),
            2 => new MailboxFullError(),
            3 => new RecipientServerNotFoundError(),
            4 => new MailboxBlockError(),
            6 => new SubscriberIsOfflineError(),
            11 => new ServiceNotConnectedError(),
            12 => new SubscriberPhoneError(),
            13 => new SubscriberBlockedError(),
            21 => new NoServiceSupportError(),
            200 => new VirtualSendingError(),
            219 => new SimCardReplacementError(),
            220 => new OperatorQueueFullError(),
            237 => new SubscriberNotRespondingError(),
            238 => new NoTemplateError(),
            239 => new ForbiddenIpAddressError(),
            240 => new SubscriberBusyError(),
            241 => new ConversionError(),
            242 => new AnsweringMachineError(),
            243 => new ContractConcludedError(),
            244 => new MailingProhibitedError(),
            245 => new StatusNotReceivedError(),
            246 => new TimeLimitError(),
            247 => new MessageLimitExceededError(),
            248 => new NoRouteError(),
            249 => new InvalidNumberFormatError(),
            250 => new SettingsNumberForbiddenError(),
            251 => new OneNumberLimitExceededError(),
            252 => new NumberForbiddenError(),
            253 => new SpamFilterProhibitedError(),
            254 => new UnregisteredSenderIdError(),
            255 => new OperatorRejectedError(),
            _ => throw new NotImplementedException($"Type with error code {code} not implemented."),
        };
    }

    public override void WriteJson(JsonWriter writer, ISmsError? value, JsonSerializer serializer)
        => writer.WriteValue(value?.Code);
}
#endif