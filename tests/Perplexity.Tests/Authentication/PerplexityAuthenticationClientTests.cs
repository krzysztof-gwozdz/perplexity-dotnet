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
        Assert.NotNull(generateResult.RawApiRequest);
        Assert.NotEmpty(generateResult.RawApiRequest.Headers);
        Assert.NotNull(generateResult.RawApiRequest.Content);
        Assert.NotEmpty(generateResult.RawApiRequest.Content);
        Assert.NotNull(generateResult.RawApiResponse);
        Assert.Equal(HttpStatusCode.OK, generateResult.RawApiResponse.StatusCode);
        Assert.NotEmpty(generateResult.RawApiResponse.Headers);
        Assert.NotNull(generateResult.RawApiResponse.Content);
        Assert.NotEmpty(generateResult.RawApiResponse.Content);
        Assert.Null(generateResult.Error);
        Assert.NotNull(generateResult.Data);
        var data = generateResult.Data;
        Assert.NotNull(data.AuthToken);
        Assert.True(data.CreatedAtEpochSeconds > 0);
        Assert.Equal(generateAuthTokenRequest.TokenName, data.TokenName);

        // act revoke auth token
        var revokeAuthTokenRequest = new RevokeAuthTokenRequest { AuthToken = generateResult.Data.AuthToken };
        var revokeResult = await authenticationClient.RevokeAuthToken(revokeAuthTokenRequest);

        // assert revoke auth token
        Assert.NotNull(generateResult);
        Assert.True(revokeResult.IsSuccess);
        Assert.NotNull(revokeResult.RawApiRequest);
        Assert.NotEmpty(revokeResult.RawApiRequest.Headers);
        Assert.NotNull(revokeResult.RawApiRequest.Content);
        Assert.NotEmpty(revokeResult.RawApiRequest.Content);
        Assert.NotNull(revokeResult.RawApiResponse);
        Assert.Equal(HttpStatusCode.OK, revokeResult.RawApiResponse.StatusCode);
        Assert.NotEmpty(revokeResult.RawApiResponse.Headers);
        Assert.Empty(revokeResult.RawApiResponse.Content);
        Assert.Null(revokeResult.Error);
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
        Assert.NotNull(result.RawApiRequest);
        Assert.NotEmpty(result.RawApiRequest.Headers);
        Assert.NotNull(result.RawApiRequest.Content);
        Assert.NotEmpty(result.RawApiRequest.Content);
        Assert.NotNull(result.RawApiResponse);
        Assert.Equal(HttpStatusCode.InternalServerError, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.RawApiResponse.Content);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.NotNull(result.Error);
        Assert.Equal(-1, result.Error.Code);
        Assert.NotEmpty(result.Error.Type);
        Assert.NotEmpty(result.Error.Message);
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
        Assert.NotNull(result.RawApiRequest);
        Assert.NotEmpty(result.RawApiRequest.Headers);
        Assert.NotNull(result.RawApiRequest.Content);
        Assert.NotEmpty(result.RawApiRequest.Content);
        Assert.NotNull(result.RawApiResponse);
        Assert.Equal(HttpStatusCode.NotFound, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.RawApiResponse.Content);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.NotNull(result.Error);
        Assert.Equal(-1, result.Error.Code);
        Assert.NotEmpty(result.Error.Type);
        Assert.NotEmpty(result.Error.Message);
    }
}
