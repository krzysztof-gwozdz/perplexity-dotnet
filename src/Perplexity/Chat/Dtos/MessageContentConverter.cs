using System.Text.Json;
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public sealed class MessageContentConverter : JsonConverter<object>
{
    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType switch
        {
            JsonTokenType.Null => null,
            JsonTokenType.String => reader.GetString(),
            _ => JsonSerializer.Deserialize<List<MessageContentChunk>>(ref reader, options)
        };

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case string s:
                writer.WriteStringValue(s);
                return;
            case MessageContentChunk part:
                JsonSerializer.Serialize(writer, new List<MessageContentChunk> { part }, options);
                return;
            case IEnumerable<MessageContentChunk> parts:
                JsonSerializer.Serialize(writer, parts.ToList(), options);
                return;
            default:
                throw new JsonException($"Message content must be string or IReadOnlyList<{nameof(MessageContentChunk)}>. Got {value?.GetType().FullName ?? "null"}.");
        }
    }
}