#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.AgenticResearch.Dtos;

public record UrlContent
{
    [JsonPropertyName("url")]
    public string Url { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; }

    [JsonPropertyName("snippet")]
    public string Snippet { get; init; }
}
