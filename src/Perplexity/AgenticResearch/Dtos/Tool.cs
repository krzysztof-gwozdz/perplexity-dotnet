#nullable disable
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Perplexity.AgenticResearch.Dtos;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(WebSearchTool), "web_search")]
[JsonDerivedType(typeof(FetchUrlTool), "fetch_url")]
[JsonDerivedType(typeof(FunctionTool), "function")]
public abstract record Tool;

public record WebSearchTool : Tool
{
    [JsonPropertyName("filters")]
    public WebSearchFilters Filters { get; init; }

    [JsonPropertyName("max_tokens")]
    public int? MaxTokens { get; init; }

    [JsonPropertyName("max_tokens_per_page")]
    public int? MaxTokensPerPage { get; init; }

    [JsonPropertyName("user_location")]
    public ToolUserLocation UserLocation { get; init; }
}

public record WebSearchFilters
{
    [JsonPropertyName("domain_filter")]
    public IReadOnlyList<string> DomainFilter { get; init; }

    [JsonPropertyName("date_filter")]
    public string DateFilter { get; init; }
}

public record ToolUserLocation
{
    [JsonPropertyName("country")]
    public string Country { get; init; }

    [JsonPropertyName("city")]
    public string City { get; init; }

    [JsonPropertyName("region")]
    public string Region { get; init; }
}

public record FetchUrlTool : Tool
{
    [JsonPropertyName("max_urls")]
    public int? MaxUrls { get; init; }
}

public record FunctionTool : Tool
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("parameters")]
    public JsonElement? Parameters { get; init; }

    [JsonPropertyName("strict")]
    public bool? Strict { get; init; }
}
