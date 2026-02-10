#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Search.Dtos;

public record SearchRequest
{
    [JsonPropertyName("query")]
    public string Query { get; init; }

    [JsonPropertyName("max_tokens")]
    public int? MaxTokens { get; init; }

    [JsonPropertyName("max_tokens_per_page")]
    public int? MaxTokensPerPage { get; init; }

    [JsonPropertyName("max_results")]
    public int? MaxResults { get; init; }

    [JsonPropertyName("search_domain_filter")]
    public IReadOnlyList<string> SearchDomainFilter { get; init; }

    [JsonPropertyName("search_language_filter")]
    public IReadOnlyList<string> SearchLanguageFilter { get; init; }

    [JsonPropertyName("search_recency_filter")]
    public string SearchRecencyFilter { get; init; }

    [JsonPropertyName("search_after_date_filter")]
    public string SearchAfterDateFilter { get; init; }

    [JsonPropertyName("search_before_date_filter")]
    public string SearchBeforeDateFilter { get; init; }

    [JsonPropertyName("last_updated_after_filter")]
    public string LastUpdatedAfterFilter { get; init; }

    [JsonPropertyName("last_updated_before_filter")]    
    public string LastUpdatedBeforeFilter { get; init; }

    // It exists in documentation, but every request with search_mode generates error "search_mode is not supported"
    // [JsonPropertyName("search_mode")]
    // public string SearchMode { get; init; }

    [JsonPropertyName("country")]
    public string Country { get; init; }

    [JsonPropertyName("display_server_time")]
    public bool? DisplayServerTime { get; init; }
}
