#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record CreateChatCompletionRequest
{
    [JsonPropertyName("model")]
    public string Model { get; init; }

    [JsonPropertyName("messages")]
    public IReadOnlyList<Message> Messages { get; init; }

    [JsonPropertyName("max_tokens")]
    public int? MaxTokens { get; init; }

    [JsonPropertyName("stream")]
    public bool? Stream { get; init; }

    [JsonPropertyName("temperature")]
    public double? Temperature { get; init; }

    [JsonPropertyName("top_p")]
    public double? TopP { get; init; }

    [JsonPropertyName("stop")]
    public IReadOnlyList<string> Stop { get; init; }

    [JsonPropertyName("response_format")]
    public ResponseFormat ResponseFormat { get; init; }

    [JsonPropertyName("web_search_options")]
    public WebSearchOptions WebSearchOptions { get; init; }

    [JsonPropertyName("search_mode")]
    public string SearchMode { get; init; }

    [JsonPropertyName("disable_search")]
    public bool? DisableSearch { get; init; }

    [JsonPropertyName("return_images")]
    public bool? ReturnImages { get; init; }

    [JsonPropertyName("return_related_questions")]
    public bool? ReturnRelatedQuestions { get; init; }

    [JsonPropertyName("enable_search_classifier")]
    public bool? EnableSearchClassifier { get; init; }

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

    [JsonPropertyName("last_updated_before_filter")]
    public string LastUpdatedBeforeFilter { get; init; }

    [JsonPropertyName("last_updated_after_filter")]
    public string LastUpdatedAfterFilter { get; init; }

    [JsonPropertyName("image_format_filter")]
    public IReadOnlyList<string> ImageFormatFilter { get; init; }

    [JsonPropertyName("image_domain_filter")]
    public IReadOnlyList<string> ImageDomainFilter { get; init; }

    [JsonPropertyName("stream_mode")]
    public string StreamMode { get; init; }

    [JsonPropertyName("reasoning_effort")]
    public string ReasoningEffort { get; init; }

    [JsonPropertyName("language_preference")]
    public string LanguagePreference { get; init; }
}
