#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record CreateChatCompletionResponse
{
    [JsonPropertyName("choices")]
    public IReadOnlyList<ChatCompletionChoice> Choices { get; init; }
}