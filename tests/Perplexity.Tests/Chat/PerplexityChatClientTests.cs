using System.Net;
using Perplexity.Chat.Dtos;
using Perplexity.Exceptions;

namespace Perplexity.Tests.Chat;

public class PerplexityChatClientTests
{
    private readonly string _apiKey = Environment.GetEnvironmentVariable("PERPLEXITY_APIKEY")
                                      ?? throw new PerplexityMissingApiKeyException();

    [Fact]
    public async Task CreateChatCompletion_WithOnlyRequiredFields_ReturnsValidResponseWithRequiredData()
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

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateChatCompletion_WithInvalidModelData_ThrowsException(string? model)
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
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

    [Fact]
    public async Task CreateAsyncChatCompletion_WithOnlyRequiredFields_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var chatClient = perplexityClient.ChatClient;
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
        var response = await chatClient.CreateAsyncChatCompletion(request);

        // assert
        Assert.NotNull(response);
        Assert.NotNull(response.Id);
        Assert.NotNull(response.CreatedAt);
        Assert.NotNull(response.Status);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateAsyncChatCompletion_WithInvalidModelData_ThrowsException(string? model)
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var chatClient = perplexityClient.ChatClient;
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
        var createAsyncChatCompletion = async () => await chatClient.CreateAsyncChatCompletion(request);

        // assert
        var exception = await Assert.ThrowsAsync<PerplexityClientException>(createAsyncChatCompletion);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        Assert.NotNull(exception.Content);
        Assert.NotNull(exception.Error);
        Assert.Equal(400, exception.Error.Code);
        Assert.NotEmpty(exception.Error.Type);
        Assert.NotEmpty(exception.Error.Message);
    }

    [Fact]
    public async Task ListAsyncChatCompletions_WithOnlyRequiredFields_ReturnsValidResponseWithRequiredData()
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
    public async Task GetAsyncChatCompletions_WithOnlyRequiredFields_ReturnsValidResponseWithRequiredData()
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
        Assert.NotNull(response.Response);
        Assert.NotNull(response.Response.Choices);
        Assert.Single(response.Response.Choices);
        var choice = response.Response.Choices[0];
        Assert.NotNull(choice);
        Assert.NotNull(choice.Message);
        Assert.Equal("assistant", choice.Message.Role);
    }

    [Fact]
    public async Task GetAsyncChatCompletions_WithNonexistentChatCompetition_ThrowsException()
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var chatClient = perplexityClient.ChatClient;
        var @params = new GetAsyncChatCompletionParams
        {
            ApiRequest = "Nonexistent chat competition"
        };

        // act
        var getAsyncChatCompletion = async () => await chatClient.GetAsyncChatCompletion(@params);

        // assert
        var exception = await Assert.ThrowsAsync<PerplexityClientException>(getAsyncChatCompletion);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        Assert.NotNull(exception.Content);
        Assert.NotNull(exception.Error);
        Assert.Equal(404, exception.Error.Code);
        Assert.NotEmpty(exception.Error.Type);
        Assert.NotEmpty(exception.Error.Message);
    }
}