using System.Net;
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
        var generateAuthTokenRequest = new GenerateAuthTokenRequest
        {
            TokenName = $"Perplexity library test token - {Guid.NewGuid():N}"
        };

        // act generate auth token
        var generateResponse = await authenticationClient.GenerateAuthToken(generateAuthTokenRequest);

        // assert generate auth token
        Assert.NotNull(generateResponse);
        Assert.NotNull(generateResponse.AuthToken);
        Assert.NotNull(generateResponse.CreatedAtEpochSeconds);
        Assert.Equal(generateAuthTokenRequest.TokenName, generateResponse.TokenName);

        // act revoke auth token
        var revokeAuthTokenRequest = new RevokeAuthTokenRequest { AuthToken = generateResponse.AuthToken };
        await authenticationClient.RevokeAuthToken(revokeAuthTokenRequest);
    }

    [Theory]
    [InlineData(".")]
    [InlineData(":")]
    [InlineData("/")]
    [InlineData("#")]
    [InlineData("*")]
    [InlineData("~")]
    public async Task GenerateAuth_WithInvalidCharactersInTokenName_ThrowsException(string? tokenName)
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var authenticationClient = perplexityClient.AuthenticationClient;
        var request = new GenerateAuthTokenRequest { TokenName = tokenName };

        // act
        var generateAuthToken = async () => await authenticationClient.GenerateAuthToken(request);

        // assert
        var exception = await Assert.ThrowsAsync<PerplexityClientException>(generateAuthToken);
        Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        Assert.Equal(
            "Token name can only contain alphanumeric characters, hyphens (-), underscores (_), at symbols (@), and spaces",
            exception.Content);
    }

    [Fact]
    public async Task RevokeAuthToken_WithNonexistentAuthToken_ThrowsException()
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var authenticationClient = perplexityClient.AuthenticationClient;
        var request = new RevokeAuthTokenRequest { AuthToken = "Nonexistent auth token" };

        // act
        var revokeAuthToken = async () => await authenticationClient.RevokeAuthToken(request);

        // assert
        var exception = await Assert.ThrowsAsync<PerplexityClientException>(revokeAuthToken);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        Assert.Equal("[delete_auth_token_internal] token not found", exception.Content);
    }
}