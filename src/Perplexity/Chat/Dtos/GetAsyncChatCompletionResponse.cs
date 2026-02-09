#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record GetAsyncChatCompletionResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("model")]
    public string Model { get; init; }

    [JsonPropertyName("created_at")]
    public long? CreatedAt { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; }

    [JsonPropertyName("started_at")]    
    public long? StartedAt { get; init; }

    [JsonPropertyName("completed_at")]
    public long? CompletedAt { get; init; }

    [JsonPropertyName("response")]
    public CompletionResponse Response { get; init; }

    [JsonPropertyName("failed_at")]
    public long? FailedAt { get; init; }

    [JsonPropertyName("error_message")]
    public string? ErrorMessage { get; init; }
}