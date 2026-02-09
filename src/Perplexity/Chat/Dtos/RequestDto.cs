#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record RequestDto<T> 
{
    [JsonPropertyName("request")]
    public T Request { get; init; }
}