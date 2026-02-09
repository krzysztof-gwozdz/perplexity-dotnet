using Perplexity.Authentication.Dtos;
using Perplexity.Exceptions;

namespace Perplexity.Tests.Authentication;

public class PerplexityAuthenticationClientTests
{
    private readonly string _apiKey = Environment.GetEnvironmentVariable("PERPLEXITY_APIKEY")
                                      ?? throw new PerplexityMissingApiKeyException();

    [Fact]
    public async Task GenerateAuthTokenAndRevokeIt_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var authenticationClient = perplexityClient.AuthenticationClient;
        var request = new GenerateAuthTokenRequest
        {
            TokenName = "Perplexity library test token"
        };

        // act generate auth token
        var generateResponse = await authenticationClient.GenerateAuthToken(request);

        // assert generate auth token
        Assert.NotNull(generateResponse);
        Assert.NotNull(generateResponse.AuthToken);
        Assert.NotNull(generateResponse.CreatedAtEpochSeconds);
        Assert.NotNull(generateResponse.TokenName);

        // act revoke auth token
        var revokeRequest = new RevokeAuthTokenRequest { AuthToken = generateResponse.AuthToken };
        await authenticationClient.RevokeAuthToken(revokeRequest);
    }
}