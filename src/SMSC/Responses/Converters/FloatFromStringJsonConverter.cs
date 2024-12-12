namespace SMSC.Responses.Converters;

internal class FloatFromStringJsonConverter : JsonConverter<float?>
{
    /// <summary>
    /// Читаем значение свойства и возвращаем строку вне зависимости от типа (строка или цифра)
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="InvalidCastException"></exception>
    public override float? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var propValue = reader.GetString() ?? string.Empty;
            if (!float.TryParse(propValue, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var result))
                return null;
            return result;
        }
        if (reader.TokenType == JsonTokenType.Number)
        {
            var propValue = reader.GetInt32().ToString();
            if (!float.TryParse(propValue, out var result))
                return null;
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
    public override void Write(Utf8JsonWriter writer, float? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
