#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record ChatCompletionChoice
{
    [JsonPropertyName("message")]
    public ChatCompletionMessage Message { get; init; }
}