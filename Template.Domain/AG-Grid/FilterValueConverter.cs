using System.Text.Json;
using System.Text.Json.Serialization;

public class FilterValueConverter : JsonConverter<object>
{

    // Required public parameterless constructor
    public FilterValueConverter() { }

    public override object Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                return reader.GetString();
            case JsonTokenType.Number:
                if (reader.TryGetInt32(out int intValue))
                    return intValue;
                if (reader.TryGetDouble(out double doubleValue))
                    return doubleValue;
                throw new JsonException($"Unsupported number format");
            case JsonTokenType.True:
                return true;
            case JsonTokenType.False:
                return false;
            case JsonTokenType.Null:
                return null;
            default:
                throw new JsonException($"Unsupported token type: {reader.TokenType}");
        }
    }

    public override void Write(
        Utf8JsonWriter writer,
        object value,
        JsonSerializerOptions options)
    {
        // Let the default serializer handle writing
        JsonSerializer.Serialize(writer, value, options);
    }
}