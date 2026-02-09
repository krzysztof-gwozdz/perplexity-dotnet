#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.AgenticResearch.Dtos;

public record AgenticResearchRequest
{
    [JsonPropertyName("input")]
    public string Input { get; init; }
    
    [JsonPropertyName("model")]
    public string Model { get; init; }
}