#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record Usage
{
    [JsonPropertyName("prompt_tokens")]
    public int? PromptTokens { get; init; }

    [JsonPropertyName("completion_tokens")]
    public int? CompletionTokens { get; init; }

    [JsonPropertyName("total_tokens")]
    public int? TotalTokens { get; init; }

    [JsonPropertyName("cost")]
    public Cost Cost { get; init; }

    [JsonPropertyName("search_context_size")]
    public string SearchContextSize { get; init; }

    [JsonPropertyName("citation_tokens")]
    public int? CitationTokens { get; init; }

    [JsonPropertyName("num_search_queries")]
    public int? NumSearchQueries { get; init; }

    [JsonPropertyName("reasoning_tokens")]
    public int? ReasoningTokens { get; init; }
}

