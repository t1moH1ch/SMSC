
namespace SMSC.Responses.Converters;

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
