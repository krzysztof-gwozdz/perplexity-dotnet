#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record ListAsyncChatCompletionsResponse
{
    [JsonPropertyName("requests")]
    public IReadOnlyList<Request> Requests { get; init; }

    [JsonPropertyName("next_token")]
    public string NextToken { get; init; }

    public record Request
    {
        [JsonPropertyName("id")]
        public string Id { get; init; }

        [JsonPropertyName("created_at")]
        public long? CreatedAt { get; init; }

        [JsonPropertyName("status")]
        public string Status { get; init; } 

        [JsonPropertyName("started_at")]
        public long? StartedAt { get; init; }

        [JsonPropertyName("completed_at")]
        public long? CompletedAt { get; init; }

        [JsonPropertyName("failed_at")]
        public long? FailedAt { get; init; }
    }
}