#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Common.Dtos;

public record Error
{
    [JsonPropertyName("code")]
    public int Code { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; }
    
    [JsonPropertyName("message")]
    public string Message { get; init; }
}

