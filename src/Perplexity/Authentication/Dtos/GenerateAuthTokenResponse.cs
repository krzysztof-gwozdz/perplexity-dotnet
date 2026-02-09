#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Authentication.Dtos;

public record GenerateAuthTokenResponse
{
    [JsonPropertyName("auth_token")]
    public string AuthToken { get; init; }

    [JsonPropertyName("created_at_epoch_seconds")]
    public long? CreatedAtEpochSeconds { get; init; }

    [JsonPropertyName("token_name")]
    public string TokenName { get; init; }
}