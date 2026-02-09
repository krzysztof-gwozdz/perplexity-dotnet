#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record ChatCompletionMessage
{
    [JsonPropertyName("role")]
    public string Role { get; init; }

    [JsonPropertyName("content")]
    public string Content { get; init; }
}