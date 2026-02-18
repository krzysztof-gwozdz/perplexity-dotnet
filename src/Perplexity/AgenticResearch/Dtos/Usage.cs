#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.AgenticResearch.Dtos;

public record Usage
{
    [JsonPropertyName("input_tokens")]
    public int? InputTokens { get; init; }

    [JsonPropertyName("output_tokens")]
    public int? OutputTokens { get; init; }

    [JsonPropertyName("total_tokens")]
    public int? TotalTokens { get; init; }

    [JsonPropertyName("input_tokens_details")]
    public InputTokensDetails InputTokensDetails { get; init; }

    [JsonPropertyName("tool_calls_details")]
    public Dictionary<string, ToolCallDetails> ToolCallsDetails { get; init; }

    [JsonPropertyName("cost")]
    public Cost Cost { get; init; }
}

public record InputTokensDetails
{
    [JsonPropertyName("cache_creation_input_tokens")]
    public int? CacheCreationInputTokens { get; init; }

    [JsonPropertyName("cache_read_input_tokens")]
    public int? CacheReadInputTokens { get; init; }
}

public record ToolCallDetails
{
    [JsonPropertyName("input_tokens")]
    public int? InputTokens { get; init; }

    [JsonPropertyName("output_tokens")]
    public int? OutputTokens { get; init; }

    [JsonPropertyName("total_tokens")]
    public int? TotalTokens { get; init; }
}
