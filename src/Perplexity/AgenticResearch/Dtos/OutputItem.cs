#nullable disable
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Perplexity.AgenticResearch.Dtos;

[JsonConverter(typeof(OutputItemConverter))]
public abstract record OutputItem
{
    [JsonPropertyName("type")]
    public string Type { get; init; }
}

public record UnknownOutputItem : OutputItem
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement> ExtensionData { get; init; }
}

public record MessageOutputItem : OutputItem
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; }

    [JsonPropertyName("role")]
    public string Role { get; init; }

    [JsonPropertyName("content")]
    public IReadOnlyList<ContentPart> Content { get; init; }
}

public record SearchResultsOutputItem : OutputItem
{
    [JsonPropertyName("results")]
    public IReadOnlyList<SearchResult> Results { get; init; }

    [JsonPropertyName("queries")]
    public IReadOnlyList<string> Queries { get; init; }
}

public record FetchUrlResultsOutputItem : OutputItem
{
    [JsonPropertyName("contents")]
    public IReadOnlyList<UrlContent> Contents { get; init; }
}

public record FunctionCallOutputItem : OutputItem
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("call_id")]
    public string CallId { get; init; }

    [JsonPropertyName("arguments")]
    public string Arguments { get; init; }

    [JsonPropertyName("thought_signature")]
    public string ThoughtSignature { get; init; }
}
