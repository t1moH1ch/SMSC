namespace SMSC.Responses.Converters;

internal class NumberToStringJsonConverter : JsonConverter<string>
{
    /// <summary>
    /// Читаем значение свойства и возвращаем строку вне зависимости от типа (строка или цифра)
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="InvalidCastException"></exception>
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
            return reader.GetString() ?? string.Empty;
        if (reader.TokenType == JsonTokenType.Number)
            return reader.GetInt32().ToString();
        throw new InvalidCastException($"Unsupported token type {Enum.GetName(reader.TokenType)}");
    }
    /// <summary>
    /// Записываем значение как строку вне зависимости от значения
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}
