#nullable disable
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Perplexity.AgenticResearch.Dtos;

public class InputConverter : JsonConverter<object>
{
    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType switch
        {
            JsonTokenType.Null => null,
            JsonTokenType.String => reader.GetString(),
            JsonTokenType.StartArray => JsonSerializer.Deserialize<IReadOnlyList<InputItem>>(ref reader, options),
            _ => throw new JsonException("Input must be a string or an array of InputItem.")
        };

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case string s:
                writer.WriteStringValue(s);
                return;
            case IEnumerable<InputItem> items:
                JsonSerializer.Serialize(writer, items, options);
                return;
            default:
                throw new JsonException($"Unexpected input type: {value?.GetType().Name}");
        }
    }
}
