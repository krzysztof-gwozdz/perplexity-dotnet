#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record Choice
{
    [JsonPropertyName("index")]
    public int Index { get; init; }

    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; init; }

    [JsonPropertyName("message")]
    public Message Message { get; init; }

    [JsonPropertyName("delta")]
    public Message Delta { get; init; }
}