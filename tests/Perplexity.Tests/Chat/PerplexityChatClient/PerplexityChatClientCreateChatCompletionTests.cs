using System.Net;
using Perplexity.Chat.Dtos;
using Perplexity.Exceptions;

namespace Perplexity.Tests.Chat.PerplexityChatClient;

public class PerplexityChatClientCreateChatCompletionTests : PerplexityChatClientTestsBase
{
    private const string Model = "sonar";

    [Fact]
    public async Task CreateChatCompletion_WithOnlyRequiredFields_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var chatClient = perplexityClient.ChatClient;
        var request = new CreateChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateSystemMessage("Response with pong for ping request"),
                Message.CreateUserMessage("ping")
            ]
        };

        // act
        var response = await chatClient.CreateChatCompletion(request);

        // assert
        ValidateValidResponse(response);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateChatCompletion_WithInvalidModelData_ThrowsException(string? model)
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var chatClient = perplexityClient.ChatClient;
        var request = new CreateChatCompletionRequest
        {
            Model = model,
            Messages =
            [
                Message.CreateSystemMessage("Response with pong for ping request"),
                Message.CreateUserMessage("ping")
            ]
        };

        // act
        var createAsyncChatCompletion = async () => await chatClient.CreateChatCompletion(request);

        // assert
        var exception = await Assert.ThrowsAsync<PerplexityClientException>(createAsyncChatCompletion);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        Assert.NotNull(exception.Content);
        Assert.NotNull(exception.Error);
        Assert.Equal(400, exception.Error.Code);
        Assert.NotEmpty(exception.Error.Type);
        Assert.NotEmpty(exception.Error.Message);
    }

    private static void ValidateValidResponse(CreateChatCompletionResponse response)
    {
        Assert.NotNull(response);
        Assert.NotNull(response.Id);
        Assert.Equal(Model, response.Model);
        Assert.NotNull(response.Created);
        Assert.NotNull(response.Choices);
        Assert.Single(response.Choices);
        foreach (var choice in response.Choices)
        {
            Assert.NotNull(choice);
            Assert.NotNull(choice.Message);
            Assert.Equal("assistant", choice.Message.Role);
            Assert.NotNull(response.Usage);
            Assert.NotNull(response.Usage.PromptTokens);
            Assert.NotNull(response.Usage.CompletionTokens);
            Assert.NotNull(response.Usage.TotalTokens);
            Assert.NotNull(response.Usage.Cost);
            Assert.NotNull(response.Usage.Cost.InputTokensCost);
            Assert.NotNull(response.Usage.Cost.OutputTokensCost);
            Assert.NotNull(response.Usage.Cost.TotalCost);
        }
    }
}