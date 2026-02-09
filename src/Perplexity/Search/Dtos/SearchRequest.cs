#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Search.Dtos;

public record SearchRequest
{
    [JsonPropertyName("query")]
    public string Query { get; init; }
}