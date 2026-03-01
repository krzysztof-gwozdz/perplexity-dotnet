#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record WebSearchOptions
{
    [JsonPropertyName("search_context_size")]
    public string SearchContextSize { get; init; }
}
