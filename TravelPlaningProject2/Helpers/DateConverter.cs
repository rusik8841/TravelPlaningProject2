using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TravelPlaningProject2.Helpers;

public class DateConverter : JsonConverter<DateTime?>
{
    private static readonly string[] DateFormats =
    {
        "yyyy-MM-dd",
        "yyyy-MM-dd HH:mm:ss",
        "yyyy-MM-ddTHH:mm:ss",
        "yyyy-MM-ddTHH:mm:ssZ",
        "yyyy-MM-ddTHH:mm:ss.fffZ"
    };

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                var dateStr = reader.GetString();
                if (string.IsNullOrWhiteSpace(dateStr))
                    return null;

                if (DateTime.TryParseExact(dateStr, DateFormats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var parsedDate))
                {
                    return parsedDate;
                }

                if (long.TryParse(dateStr, out var timestamp))
                    return DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;

                break;

            case JsonTokenType.Number:
                if (reader.TryGetInt64(out var unixTimestamp))
                    return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).DateTime;
                break;

            case JsonTokenType.Null:
                return null;
        }

        throw new JsonException($"Cannot convert value to DateTime: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString("yyyy-MM-dd HH:mm:ss"));
        else
            writer.WriteNullValue();
    }
}