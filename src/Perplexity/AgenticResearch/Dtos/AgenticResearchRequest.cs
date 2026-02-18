#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.AgenticResearch.Dtos;

public record AgenticResearchRequest
{
    [JsonPropertyName("input")]
    [JsonConverter(typeof(InputConverter))]
    public object Input { get; init; }

    [JsonPropertyName("model")]
    public string Model { get; init; }

    [JsonPropertyName("instructions")]
    public string Instructions { get; init; }

    [JsonPropertyName("language_preference")]
    public string LanguagePreference { get; init; }

    [JsonPropertyName("max_output_tokens")]
    public int? MaxOutputTokens { get; init; }

    [JsonPropertyName("max_steps")]
    public int? MaxSteps { get; init; }

    [JsonPropertyName("models")]
    public IReadOnlyList<string> Models { get; init; }

    [JsonPropertyName("preset")]
    public string Preset { get; init; }

    [JsonPropertyName("reasoning")]
    public ReasoningConfig Reasoning { get; init; }

    [JsonPropertyName("response_format")]
    public ResponseFormat ResponseFormat { get; init; }

    [JsonPropertyName("stream")]
    public bool? Stream { get; init; }

    [JsonPropertyName("tools")]
    public IReadOnlyList<Tool> Tools { get; init; }
}
