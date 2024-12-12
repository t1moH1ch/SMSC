namespace SMSC.Responses.Converters;

public class StringToDateTimeJsonConverter : JsonConverter<DateTime?>
{
    /// <summary>
    /// Читаем значение свойства и возвращаем дату
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="InvalidCastException"></exception>
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var propValue = reader.GetString() ?? string.Empty;
            if (!DateTime.TryParse(propValue, out var result))
                return null;
            return result;
        }
        if (reader.TokenType == JsonTokenType.Number)
        {
            var propValue = reader.GetInt64();
            var result = DateTime.UnixEpoch.AddSeconds(propValue).ToLocalTime();
            return result;
        }
        throw new InvalidCastException($"Unsupported token type {Enum.GetName(reader.TokenType)}");
    }
    /// <summary>
    /// Записываем значение как строку вне зависимости от значения
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
