using System.Net;
using Perplexity.Authentication.Dtos;

namespace Perplexity.Tests.Authentication;

public class PerplexityAuthenticationClientTests
{
    [Fact]
    public async Task GenerateAuthTokenAndRevokeIt_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var authenticationClient = perplexityClient.AuthenticationClient;
        var generateAuthTokenRequest = new GenerateAuthTokenRequest
        {
            TokenName = $"Perplexity library test token - {Guid.NewGuid():N}"
        };

        // act generate auth token
        var generateResult = await authenticationClient.GenerateAuthToken(generateAuthTokenRequest);

        // assert generate auth token
        Assert.NotNull(generateResult);
        Assert.True(generateResult.IsSuccess);
        Assert.NotNull(generateResult.RawApiResponse);
        Assert.NotEmpty(generateResult.RawApiResponse.Content);
        Assert.Equal(HttpStatusCode.OK, generateResult.RawApiResponse.StatusCode);
        Assert.NotEmpty(generateResult.RawApiResponse.Headers);
        Assert.NotNull(generateResult.Data);
        Assert.NotNull(generateResult.Data.AuthToken);
        Assert.NotNull(generateResult.Data.CreatedAtEpochSeconds);
        Assert.Equal(generateAuthTokenRequest.TokenName, generateResult.Data.TokenName);

        // act revoke auth token
        var revokeAuthTokenRequest = new RevokeAuthTokenRequest { AuthToken = generateResult.Data.AuthToken };
        var revokeResult = await authenticationClient.RevokeAuthToken(revokeAuthTokenRequest);

        // assert revoke auth token
        Assert.NotNull(revokeResult);
        Assert.True(revokeResult.IsSuccess);
        Assert.NotNull(revokeResult.RawApiResponse);
    }

    [Theory]
    [InlineData(".")]
    [InlineData(":")]
    [InlineData("/")]
    [InlineData("#")]
    [InlineData("*")]
    [InlineData("~")]
    public async Task GenerateAuth_WithInvalidCharactersInTokenName_ReturnsFailResponse(string? tokenName)
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var authenticationClient = perplexityClient.AuthenticationClient;
        var request = new GenerateAuthTokenRequest { TokenName = tokenName };

        // act
        var result = await authenticationClient.GenerateAuthToken(request);

        // assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.RawApiResponse);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Equal(HttpStatusCode.InternalServerError, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
    }

    [Fact]
    public async Task RevokeAuthToken_WithNonexistentAuthToken_ReturnsFailResponse()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var authenticationClient = perplexityClient.AuthenticationClient;
        var request = new RevokeAuthTokenRequest { AuthToken = "Nonexistent auth token" };

        // act
        var result = await authenticationClient.RevokeAuthToken(request);

        // assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.RawApiResponse);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Equal(HttpStatusCode.NotFound, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
    }
}
