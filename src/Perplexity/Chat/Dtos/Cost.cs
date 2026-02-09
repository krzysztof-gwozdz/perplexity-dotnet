#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record Cost
{
    [JsonPropertyName("input_tokens_cost")]
    public double? InputTokensCost { get; init; }

    [JsonPropertyName("output_tokens_cost")]
    public double? OutputTokensCost { get; init; }

    [JsonPropertyName("total_cost")]
    public double? TotalCost { get; init; }

    [JsonPropertyName("reasoning_tokens_cost")]
    public double? ReasoningTokensCost { get; init; }

    [JsonPropertyName("request_cost")]
    public double? RequestCost { get; init; }

    [JsonPropertyName("citation_tokens_cost")]
    public double? CitationTokensCost { get; init; }

    [JsonPropertyName("search_queries_cost")]
    public double? SearchQueriesCost { get; init; }
}

