#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Embeddings.Dtos;

public record EmbeddingObject
{
    [JsonPropertyName("object")]
    public string Object { get; init; }

    [JsonPropertyName("index")]
    public int Index { get; init; }

    [JsonPropertyName("embedding")]
    public string Embedding { get; init; }
}
