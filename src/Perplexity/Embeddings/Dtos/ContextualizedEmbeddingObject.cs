#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Embeddings.Dtos;

public record ContextualizedEmbeddingObject
{
    [JsonPropertyName("object")]
    public string Object { get; init; }

    [JsonPropertyName("index")]
    public int Index { get; init; }

    [JsonPropertyName("data")]
    public IReadOnlyList<EmbeddingObject> Data { get; init; }
}
