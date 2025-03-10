#if NET_FRAMEWORK_AND_NET_STANDARD
using Newtonsoft.Json;
#endif

namespace SMSC.Responses.Converters;

#if NET_CORE_APP
internal class StringToStatusJsonConverter : JsonConverter<IStatusCode>
{
    public override IStatusCode? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
            -3 => new MessageNotFound(),
            -2 => new Stopped(),
            -1 => new DeliveryWaiting(),
            0 => new OperatorPassed(),
            1 => new Delivered(),
            2 => new Read(),
            3 => new Overdue(),
            4 => new LinkClicked(),
            20 => new ImpossibleDeliver(),
            22 => new InvalidNumber(),
            23 => new Forbidden(),
            24 => new InsufficientFunds(),
            25 => new UnavailableNumber(),
            _ => throw new NotImplementedException($"Type with code {code} not implemented."),
        };
    }

    public override void Write(Utf8JsonWriter writer, IStatusCode value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Code.ToString());
    }
}
#else
internal class StringToStatusJsonConverter : JsonConverter<IStatusCode>
{
    public override IStatusCode? ReadJson(JsonReader reader, Type objectType, IStatusCode? existingValue, bool hasExistingValue, JsonSerializer serializer)
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
            -3 => new MessageNotFound(),
            -2 => new Stopped(),
            -1 => new DeliveryWaiting(),
            0 => new OperatorPassed(),
            1 => new Delivered(),
            2 => new Read(),
            3 => new Overdue(),
            4 => new LinkClicked(),
            20 => new ImpossibleDeliver(),
            22 => new InvalidNumber(),
            23 => new Forbidden(),
            24 => new InsufficientFunds(),
            25 => new UnavailableNumber(),
            _ => throw new NotImplementedException($"Type with code {code} not implemented."),
        };
    }

    public override void WriteJson(JsonWriter writer, IStatusCode? value, JsonSerializer serializer)
        => writer.WriteValue(value?.Code.ToString());
}
#endif