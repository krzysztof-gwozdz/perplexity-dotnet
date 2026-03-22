#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Embeddings.Dtos;

public record EmbeddingsUsage
{
    [JsonPropertyName("prompt_tokens")]
    public int? PromptTokens { get; init; }

    [JsonPropertyName("total_tokens")]
    public int? TotalTokens { get; init; }

    [JsonPropertyName("cost")]
    public EmbeddingsCost Cost { get; init; }
}
