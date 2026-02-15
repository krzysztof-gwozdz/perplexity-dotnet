using System.Net;
using Perplexity.Chat.Dtos;
using Perplexity.Exceptions;

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
        var response = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        ValidateValidResponse(response);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateAsyncChatCompletion_WithInvalidModelData_ThrowsException(string? model)
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
        var createAsyncChatCompletion = async () => await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        var exception = await Assert.ThrowsAsync<PerplexityClientException>(createAsyncChatCompletion);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        Assert.NotNull(exception.Content);
        Assert.NotNull(exception.Error);
        Assert.Equal(400, exception.Error.Code);
        Assert.NotEmpty(exception.Error.Type);
        Assert.NotEmpty(exception.Error.Message);
    }

    private static void ValidateValidResponse(CreateAsyncChatCompletionResponse response)
    {
        Assert.NotNull(response);
        Assert.NotNull(response.Id);
        Assert.NotNull(response.CreatedAt);
        Assert.NotNull(response.Status);
    }
}