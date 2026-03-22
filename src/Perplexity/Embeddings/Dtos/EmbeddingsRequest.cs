#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Embeddings.Dtos;

public record EmbeddingsRequest
{
    [JsonPropertyName("input")]
    public IReadOnlyList<string> Input { get; init; }

    [JsonPropertyName("model")]
    public string Model { get; init; }

    [JsonPropertyName("dimensions")]
    public int? Dimensions { get; init; }

    [JsonPropertyName("encoding_format")]
    public string EncodingFormat { get; init; }
}
