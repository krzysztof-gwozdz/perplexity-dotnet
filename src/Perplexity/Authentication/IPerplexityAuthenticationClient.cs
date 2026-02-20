using Perplexity.Authentication.Dtos;
using Perplexity.Common;

namespace Perplexity.Authentication;

public interface IPerplexityAuthenticationClient
{
    Task<Result<GenerateAuthTokenResponse>> GenerateAuthToken(GenerateAuthTokenRequest request, CancellationToken cancellationToken = default);
    Task<Result> RevokeAuthToken(RevokeAuthTokenRequest request, CancellationToken cancellationToken = default);
}