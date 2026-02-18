#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.AgenticResearch.Dtos;

public record ContentPart
{
    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("text")]
    public string Text { get; init; }

    [JsonPropertyName("annotations")]
    public IReadOnlyList<Annotation> Annotations { get; init; }
}

public record Annotation
{
    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("url")]
    public string Url { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; }

    [JsonPropertyName("start_index")]
    public int? StartIndex { get; init; }

    [JsonPropertyName("end_index")]
    public int? EndIndex { get; init; }
}
