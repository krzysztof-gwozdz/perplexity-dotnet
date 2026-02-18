#nullable disable
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Perplexity.AgenticResearch.Dtos;

public record ResponseFormat
{
    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("json_schema")]
    public JsonSchemaFormat JsonSchema { get; init; }
}

public record JsonSchemaFormat
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("schema")]
    public JsonElement? Schema { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("strict")]
    public bool? Strict { get; init; }
}
