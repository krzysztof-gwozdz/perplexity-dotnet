#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Embeddings.Dtos;

public record EmbeddingsResponse
{
    [JsonPropertyName("object")]
    public string Object { get; init; }

    [JsonPropertyName("data")]
    public IReadOnlyList<EmbeddingObject> Data { get; init; }

    [JsonPropertyName("model")]
    public string Model { get; init; }

    [JsonPropertyName("usage")]
    public EmbeddingsUsage Usage { get; init; }
}
