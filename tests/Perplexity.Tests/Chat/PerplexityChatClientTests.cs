using Perplexity.Chat.Dtos;
using Perplexity.Exceptions;

namespace Perplexity.Tests.Chat;

public class PerplexityChatClientTests
{
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
            Model = "sonar",
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
        Assert.Equal(request.Model, response.Model);
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

    [Fact]
    public async Task CreateAsyncChatCompletion_WithOnlyRequiredData_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var chatClient = perplexityClient.ChatClient;
        var request = new CreateAsyncChatCompletionRequest()
        {
            Model = "sonar-deep-research",
            Messages =
            [
                Message.CreateSystemMessage("Response with pong for ping request"),
                Message.CreateUserMessage("ping")
            ]
        };

        // act
        var response = await chatClient.CreateAsyncChatCompletion(request);

        // assert
        Assert.NotNull(response);
        Assert.NotNull(response.Id);
        Assert.NotNull(response.CreatedAt);
        Assert.NotNull(response.Status);
    }

    [Fact]
    public async Task ListAsyncChatCompletions_WithOnlyRequiredData_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var chatClient = perplexityClient.ChatClient;

        // act
        var response = await chatClient.ListAsyncChatCompletions();

        // assert
        Assert.NotNull(response);
        Assert.NotNull(response.Requests);
        Assert.NotEmpty(response.Requests);
        var request = response.Requests[0];
        Assert.NotNull(request);
        Assert.NotNull(request.Id);
        Assert.NotNull(request.CreatedAt);
        Assert.NotNull(request.Status);
    }

    [Fact]
    public async Task GetAsyncChatCompletions_WithOnlyRequiredData_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var chatClient = perplexityClient.ChatClient;
        var asyncChatCompletions = await chatClient.ListAsyncChatCompletions();
        var @params = new GetAsyncChatCompletionParams
        {
            ApiRequest = asyncChatCompletions.Requests.Last().Id
        };

        // act
        var response = await chatClient.GetAsyncChatCompletion(@params);

        // assert
        Assert.NotNull(response);
        Assert.NotNull(response.Id);
        Assert.NotNull(response.CreatedAt);
        Assert.NotNull(response.Status);
        Assert.NotNull(response.Response.Choices);
        Assert.Single(response.Response.Choices);
        var choice = response.Response.Choices[0];
        Assert.NotNull(choice);
        Assert.NotNull(choice.Message);
        Assert.Equal("assistant", choice.Message.Role);
    }
}