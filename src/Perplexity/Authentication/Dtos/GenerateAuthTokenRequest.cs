#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Authentication.Dtos;

public record GenerateAuthTokenRequest
{
    [JsonPropertyName("token_name")]
    public string TokenName { get; init; }
}