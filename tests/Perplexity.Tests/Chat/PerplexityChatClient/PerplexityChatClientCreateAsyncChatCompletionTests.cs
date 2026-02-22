using System.Net;
using Perplexity.Chat.Dtos;

namespace Perplexity.Tests.Chat.PerplexityChatClient;

public class PerplexityChatClientCreateAsyncChatCompletionTests : PerplexityChatClientTestsBase
{
    [Fact]
    public async Task CreateAsyncChatCompletion_WithOnlyRequiredFields_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateAsyncChatCompletionRequest
        {
            Model = "sonar-deep-research",
            Messages =
            [
                Message.CreateSystemMessage("Response with pong for ping request"),
                Message.CreateUserMessage("ping")
            ]
        };

        // act
        var result = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.RawApiRequest);
        Assert.NotEmpty(result.RawApiRequest.Headers);
        Assert.NotNull(result.RawApiRequest.Content);
        Assert.NotEmpty(result.RawApiRequest.Content);
        Assert.NotNull(result.RawApiResponse);
        Assert.Equal(HttpStatusCode.OK, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.RawApiResponse.Content);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Null(result.Error);
        Assert.NotNull(result.Data);
        var data = result.Data;
        Assert.NotNull(data.Id);
        Assert.NotNull(data.CreatedAt);
        Assert.NotNull(data.Status);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateAsyncChatCompletion_WithInvalidModelData_ReturnsFailResponse(string? model)
    {
        // arrange
        var request = new CreateAsyncChatCompletionRequest
        {
            Model = model,
            Messages =
            [
                Message.CreateSystemMessage("Response with pong for ping request"),
                Message.CreateUserMessage("ping")
            ]
        };

        // act
        var result = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.RawApiRequest);
        Assert.NotEmpty(result.RawApiRequest.Headers);
        Assert.NotNull(result.RawApiRequest.Content);
        Assert.NotEmpty(result.RawApiRequest.Content);
        Assert.NotNull(result.RawApiResponse);
        Assert.Equal(HttpStatusCode.BadRequest, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.RawApiResponse.Content);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.NotNull(result.Error);
        Assert.Equal(400, result.Error.Code);
        Assert.NotEmpty(result.Error.Type);
        Assert.NotEmpty(result.Error.Message);
        Assert.Null(result.Error.Exception);
        Assert.Null(result.Data);
    }
}
