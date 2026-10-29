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
        else throw new InvalidCastException($"Unknown type of {nameof(SmsType)}. Represented type - {Enum.GetName(typeof(JsonTokenType), reader.TokenType)}");

        return type switch
        {
            0 => SmsType.Default,
            1 => SmsType.Flash,
            2 => SmsType.Bin,
            3 => SmsType.WapPush,
            4 => SmsType.HLR,
            5 => SmsType.Ping,
            6 => SmsType.Telegram,
            7 => SmsType.Call,
            8 => SmsType.Mail,
            10 => SmsType.Viber,
            12 => SmsType.Socials,
            13 => SmsType.Bots,
            _ => throw new InvalidOperationException($"Unknown type of {nameof(SmsType)}. Get code - {type}.")
        };
    }

    public override void Write(Utf8JsonWriter writer, SmsType? value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        // https://smsc.ru/api/http/status_messages/ — поле type.
        // MMS отправляется как mms=1 и отдельного кода в статусе больше нет.
        var code = value.Value switch
        {
            SmsType.Default => 0,
            SmsType.Flash => 1,
            SmsType.Bin => 2,
            SmsType.WapPush => 3,
            SmsType.HLR => 4,
            SmsType.Ping => 5,
            SmsType.Telegram => 6,
            SmsType.Call => 7,
            SmsType.Mail => 8,
            SmsType.Viber => 10,
            SmsType.Socials => 12,
            SmsType.WhatsApp or SmsType.Bots => 13,
            SmsType.MMS => throw new InvalidOperationException("SMSC status API does not assign a type code to MMS."),
            _ => throw new InvalidOperationException($"Unknown type of {nameof(SmsType)}.")
        };

        writer.WriteNumberValue(code);
    }
}
