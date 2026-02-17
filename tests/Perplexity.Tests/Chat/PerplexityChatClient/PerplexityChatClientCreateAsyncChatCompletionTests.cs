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
        ValidateSuccessfulResult(result);
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
        Assert.NotNull(result.RawApiResponse);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Equal(HttpStatusCode.BadRequest, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.Error);
        Assert.Equal(400, result.Error.Code);
        Assert.NotEmpty(result.Error.Type);
        Assert.NotEmpty(result.Error.Message);
    }

    private static void ValidateSuccessfulResult(Result<CreateAsyncChatCompletionResponse> result)
    {
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.RawApiResponse);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Equal(HttpStatusCode.OK, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.Id);
        Assert.NotNull(result.Data.CreatedAt);
        Assert.NotNull(result.Data.Status);
    }
}
