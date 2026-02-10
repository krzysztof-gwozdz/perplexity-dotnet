using Perplexity.Authentication.Dtos;
using Perplexity.Common;

namespace Perplexity.Authentication;

public class PerplexityAuthenticationClient(HttpClient httpClient, string apiKey) : BaseClient(httpClient, apiKey)
{
    public async Task<GenerateAuthTokenResponse> GenerateAuthToken(GenerateAuthTokenRequest request, CancellationToken cancellationToken = default)
    {
        var response = await Post<GenerateAuthTokenRequest, GenerateAuthTokenResponse>("/generate_auth_token", request, cancellationToken);
        return response;
    }

    public async Task RevokeAuthToken(RevokeAuthTokenRequest request, CancellationToken cancellationToken = default)
    {
        await Post("/revoke_auth_token", request, cancellationToken);
    }
}