#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Embeddings.Dtos;

public record EmbeddingsCost
{
    [JsonPropertyName("input_cost")]
    public double? InputCost { get; init; }

    [JsonPropertyName("total_cost")]
    public double? TotalCost { get; init; }

    [JsonPropertyName("currency")]
    public string Currency { get; init; }
}
