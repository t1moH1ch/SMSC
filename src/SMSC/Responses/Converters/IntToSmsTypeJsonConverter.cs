#if NET_FRAMEWORK_AND_NET_STANDARD
using Newtonsoft.Json;
#endif

namespace SMSC.Responses.Converters;

#if NET_CORE_APP
internal class IntToSmsTypeJsonConverter : JsonConverter<SmsType?>
{
    public override SmsType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        int type;
        if (reader.TokenType == JsonTokenType.String)
        {
            if (!int.TryParse(reader.GetString(), out type))
                return null;
        }
        else if (reader.TokenType == JsonTokenType.Number)
            type = reader.GetInt32();
        else throw new InvalidCastException($"Unknown type of {nameof(SmsType)}. Represented type - {Enum.GetName(reader.TokenType)}");

        return type switch
        {
            0 => SmsType.Default,
            1 => SmsType.Flash,
            2 => SmsType.Bin,
            3 => SmsType.WapPush,
            4 => SmsType.HLR,
            5 => SmsType.Ping,
            6 => SmsType.MMS,
            7 => SmsType.Call,
            8 => SmsType.Mail,
            10 => SmsType.Viber,
            12 => SmsType.Socials,
            _ => throw new InvalidOperationException($"Unknown type of {nameof(SmsType)}. Get code - {type}.")
        };
    }

    public override void Write(Utf8JsonWriter writer, SmsType? value, JsonSerializerOptions options)
    {
        switch (value)
        {
            default:
            case SmsType.Default: writer.WriteNumberValue(0); break;
            case SmsType.Flash: writer.WriteNumberValue(1); break;
            case SmsType.Bin: writer.WriteNumberValue(2); break;
            case SmsType.WapPush: writer.WriteNumberValue(3); break;
            case SmsType.HLR: writer.WriteNumberValue(4); break;
            case SmsType.Ping: writer.WriteNumberValue(5); break;
            case SmsType.MMS: writer.WriteNumberValue(6); break;
            case SmsType.Call: writer.WriteNumberValue(7); break;
            case SmsType.Mail: writer.WriteNumberValue(8); break;
            case SmsType.Viber: writer.WriteNumberValue(9); break;
            case SmsType.Socials: writer.WriteNumberValue(10); break;
        }
    }
}
#else
internal class IntToSmsTypeJsonConverter : JsonConverter<SmsType?>
{
    public override SmsType? ReadJson(JsonReader reader, Type objectType, SmsType? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        int type;
        if (reader.TokenType == JsonToken.String)
        {
            if (!int.TryParse(reader.ReadAsString(), out type))
                return null;
        }
        else if (reader.TokenType == JsonToken.Integer)
        {
            var t = reader.ReadAsInt32();
            if (t is not null)
                type = t.Value;
            else throw new InvalidCastException($"Unknown type of {nameof(SmsType)}. Represented type - {Enum.GetName(typeof(JsonToken), reader.TokenType)}");
        }
        else throw new InvalidCastException($"Unknown type of {nameof(SmsType)}. Represented type - {Enum.GetName(typeof(JsonToken), reader.TokenType)}");

        return type switch
        {
            0 => SmsType.Default,
            1 => SmsType.Flash,
            2 => SmsType.Bin,
            3 => SmsType.WapPush,
            4 => SmsType.HLR,
            5 => SmsType.Ping,
            6 => SmsType.MMS,
            7 => SmsType.Call,
            8 => SmsType.Mail,
            10 => SmsType.Viber,
            12 => SmsType.Socials,
            _ => throw new InvalidOperationException($"Unknown type of {nameof(SmsType)}. Get code - {type}.")
        };
    }

    public override void WriteJson(JsonWriter writer, SmsType? value, Newtonsoft.Json.JsonSerializer serializer)
    {
        switch (value)
        {
            default:
            case SmsType.Default: writer.WriteValue(0); break;
            case SmsType.Flash: writer.WriteValue(1); break;
            case SmsType.Bin: writer.WriteValue(2); break;
            case SmsType.WapPush: writer.WriteValue(3); break;
            case SmsType.HLR: writer.WriteValue(4); break;
            case SmsType.Ping: writer.WriteValue(5); break;
            case SmsType.MMS: writer.WriteValue(6); break;
            case SmsType.Call: writer.WriteValue(7); break;
            case SmsType.Mail: writer.WriteValue(8); break;
            case SmsType.Viber: writer.WriteValue(9); break;
            case SmsType.Socials: writer.WriteValue(10); break;
        }
    }
}
#endif