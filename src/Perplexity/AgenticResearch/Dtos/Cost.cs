#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.AgenticResearch.Dtos;

public record Cost
{
    [JsonPropertyName("currency")]
    public string Currency { get; init; }

    [JsonPropertyName("input_cost")]
    public double? InputCost { get; init; }

    [JsonPropertyName("output_cost")]
    public double? OutputCost { get; init; }

    [JsonPropertyName("total_cost")]
    public double? TotalCost { get; init; }

    [JsonPropertyName("cache_creation_cost")]
    public double? CacheCreationCost { get; init; }

    [JsonPropertyName("cache_read_cost")]
    public double? CacheReadCost { get; init; }

    [JsonPropertyName("tool_calls_cost")]
    public double? ToolCallsCost { get; init; }
}
