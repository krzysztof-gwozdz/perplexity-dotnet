#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.AgenticResearch.Dtos;

public record ReasoningConfig
{
    [JsonPropertyName("effort")]
    public string Effort { get; init; }
}
