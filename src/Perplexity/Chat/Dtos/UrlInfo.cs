#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record UrlInfo
{
    [JsonPropertyName("url")]
    public string Url { get; init; }
}
