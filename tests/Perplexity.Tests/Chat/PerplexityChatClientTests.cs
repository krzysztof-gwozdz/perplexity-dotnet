using Perplexity.Chat.Dtos;
using Perplexity.Exceptions;

namespace Perplexity.Tests.Chat;

public class PerplexityChatClientTests
{
    private const string Model = "sonar";

    private readonly string _apiKey = Environment.GetEnvironmentVariable("PERPLEXITY_APIKEY")
                                      ?? throw new PerplexityMissingApiKeyException();

    [Fact]
    public async Task CreateChatCompletion_WithOnlyRequiredData_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
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
        Assert.NotNull(response);
        Assert.NotNull(response.Id);
        Assert.Equal(Model, response.Model);
        Assert.NotNull(response.Created);
        Assert.NotNull(response.Choices);
        Assert.Single(response.Choices);
        var choice = response.Choices[0];
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