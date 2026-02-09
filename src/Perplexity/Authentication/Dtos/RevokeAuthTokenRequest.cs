#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Authentication.Dtos;

public record RevokeAuthTokenRequest
{
    [JsonPropertyName("auth_token")]
    public string AuthToken { get; init; }
}