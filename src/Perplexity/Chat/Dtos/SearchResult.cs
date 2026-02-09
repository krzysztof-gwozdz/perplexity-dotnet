#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record SearchResult
{
    [JsonPropertyName("title")]
    public string Title { get; init; }

    [JsonPropertyName("url")]
    public string Url { get; init; }

    [JsonPropertyName("date")]
    public string Date { get; init; }

    [JsonPropertyName("last_updated")]
    public string LastUpdated { get; init; }

    [JsonPropertyName("snippet")]
    public string Snippet { get; init; }

    [JsonPropertyName("source")]
    public string Source { get; init; }
}

