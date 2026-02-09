#nullable disable
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Perplexity.AgenticResearch.Dtos;

public record AgenticResearchResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("model")]
    public string Model { get; init; }

    [JsonPropertyName("created_at")]
    public long? CreatedAt { get; init; }

    [JsonPropertyName("object")]
    public string Object { get; init; }

    [JsonPropertyName("output")]
    public IReadOnlyList<JsonElement> Output { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; }
}