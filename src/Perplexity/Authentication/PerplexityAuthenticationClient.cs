using Perplexity.Authentication.Dtos;
using Perplexity.Common;

namespace Perplexity.Authentication;

public class PerplexityAuthenticationClient(HttpClient httpClient, string apiKey) : BaseClient(httpClient, apiKey), IPerplexityAuthenticationClient
{
    public async Task<Result<GenerateAuthTokenResponse>> GenerateAuthToken(GenerateAuthTokenRequest request, CancellationToken cancellationToken = default) => 
        await Post<GenerateAuthTokenRequest, GenerateAuthTokenResponse>("/generate_auth_token", request, cancellationToken);

    public async Task<Result> RevokeAuthToken(RevokeAuthTokenRequest request, CancellationToken cancellationToken = default) => 
        await Post("/revoke_auth_token", request, cancellationToken);
}
