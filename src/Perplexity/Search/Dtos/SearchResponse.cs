#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Search.Dtos;

public record SearchResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("results")] 
    public IReadOnlyList<SearchResult> Results { get; init; }

    [JsonPropertyName("server_time")] 
    public string ServerTime { get; init; }
}