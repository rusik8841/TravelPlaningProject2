using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TravelPlaningProject2.Helpers;

public class StringNotEmptyConverter : JsonConverter<string>
{
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        if (reader.TokenType == JsonTokenType.Null)
            return null;

        throw new JsonException($"Expected string token but got {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        if (string.IsNullOrWhiteSpace(value))
            writer.WriteNullValue();
        else
            writer.WriteStringValue(value);
    }
}