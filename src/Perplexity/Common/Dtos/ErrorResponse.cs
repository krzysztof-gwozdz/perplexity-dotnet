#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Common.Dtos;

public record ErrorResponse
{
    [JsonPropertyName("error")]
    public Error Error { get; init; }
}