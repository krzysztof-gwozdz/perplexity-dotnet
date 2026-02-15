using System.Text.Json;
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public sealed class MessageContentConverter : JsonConverter<object>
{
    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        if (reader.TokenType == JsonTokenType.String)
        {
            return reader.GetString();
        }
        return JsonSerializer.Deserialize<List<MessageContentChunk>>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        if (value is string s)
        {
            writer.WriteStringValue(s);
            return;
        }
        if (value is MessageContentChunk part)
        {
            JsonSerializer.Serialize(writer, new List<MessageContentChunk> { part }, options);
            return;
        }
        if (value is IEnumerable<MessageContentChunk> parts)
        {
            JsonSerializer.Serialize(writer, parts.ToList(), options);
            return;
        }
        throw new JsonException($"Message content must be string or IReadOnlyList<{nameof(MessageContentChunk)}>. Got {value?.GetType().FullName ?? "null"}.");
    }
}