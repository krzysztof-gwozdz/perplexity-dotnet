#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record CreateAsyncChatCompletionRequest
{
    [JsonPropertyName("model")]
    public string Model { get; init; }

    [JsonPropertyName("messages")]
    public IReadOnlyList<Message> Messages { get; init; }
}