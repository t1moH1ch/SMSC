#if NET_FRAMEWORK_AND_NET_STANDARD
using Newtonsoft.Json;
#endif

namespace SMSC.Responses.Converters;

#if NET_CORE_APP
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
#else
internal class NumberToStringJsonConverter : JsonConverter<string>
{
    /// <summary>
    /// Читаем значение свойства и возвращаем строку вне зависимости от типа (строка или цифра)
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="objectType"></param>
    /// <param name="existingValue"></param>
    /// <param name="hasExistingValue"></param>
    /// <param name="serializer"></param>
    /// <returns></returns>
    /// <exception cref="InvalidCastException"></exception>
    public override string? ReadJson(JsonReader reader, Type objectType, string? existingValue, bool hasExistingValue, JsonSerializer serializer)
        => reader.ReadAsString();

    /// <summary>
    /// Записываем значение как строку вне зависимости от значения
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    public override void WriteJson(JsonWriter writer, string? value, JsonSerializer serializer)
        => writer.WriteValueAsync(value);
}
#endif