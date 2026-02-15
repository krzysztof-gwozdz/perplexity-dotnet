#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

/// <summary>Video URL with optional frame interval (default 25).</summary>
public record VideoUrlInfo
{
    [JsonPropertyName("url")]
    public string Url { get; init; }

    [JsonPropertyName("frame_interval")]
    public int FrameInterval { get; init; } = 25;
}
