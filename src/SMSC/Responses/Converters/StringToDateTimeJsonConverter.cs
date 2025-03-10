#if NET_FRAMEWORK_AND_NET_STANDARD
using Newtonsoft.Json;
#endif

namespace SMSC.Responses.Converters;

#if NET_CORE_APP
/// <summary>
/// JSON конвертер из строки в <see cref="DateTime"/> формат
/// </summary>
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
#else
/// <summary>
/// JSON конвертер из строки в <see cref="DateTime"/> формат
/// </summary>
public class StringToDateTimeJsonConverter : JsonConverter<DateTime?>
{
    /// <summary>
    /// Читаем значение свойства и возвращаем дату
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="objectType"></param>
    /// <param name="existingValue"></param>
    /// <param name="hasExistingValue"></param>
    /// <param name="serializer"></param>
    /// <returns></returns>
    /// <exception cref="InvalidCastException"></exception>
    public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String | reader.TokenType == JsonToken.Date)
            return reader.ReadAsDateTime();
        
        var token = reader.ReadAsString();
        if(!long.TryParse(token, out var propValue))
            throw new InvalidCastException($"Unsupported token type {Enum.GetName(typeof(JsonToken), reader.TokenType)}");

        var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return unixEpoch.AddSeconds(propValue).ToLocalTime();
    }

    /// <summary>
    /// Записываем значение как строку вне зависимости от значения
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
        => writer.WriteValue(value.ToString());
}
#endif