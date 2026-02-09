#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record CreateChatCompletionResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("model")]
    public string Model { get; init; }

    [JsonPropertyName("created")]
    public long? Created { get; init; }

    [JsonPropertyName("choices")]
    public IReadOnlyList<Choice> Choices { get; init; }

    [JsonPropertyName("usage")]
    public UsageInfo Usage { get; init; }

    [JsonPropertyName("object")]
    public string Object { get; init; }

    [JsonPropertyName("citations")]
    public IReadOnlyList<string> Citations { get; init; }

    [JsonPropertyName("search_results")]
    public IReadOnlyList<SearchResult> SearchResults { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; }
}