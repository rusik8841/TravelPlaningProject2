using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TravelPlaningProject2.Helpers;

public class CurrencyToStringConverter : JsonConverter<decimal?>
{
    public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                var valueStr = reader.GetString()?.Trim();
                if (string.IsNullOrEmpty(valueStr))
                    return null;

                valueStr = valueStr.Replace(',', '.');
                if (decimal.TryParse(valueStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                    return Math.Round(result, 2);
                break;

            case JsonTokenType.Number:
                if (reader.TryGetDecimal(out var numValue))
                    return Math.Round(numValue, 2);
                break;

            case JsonTokenType.Null:
                return null;
        }

        throw new JsonException($"Cannot convert value to decimal: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteNumberValue(value.Value);
        else
            writer.WriteNullValue();
    }
}