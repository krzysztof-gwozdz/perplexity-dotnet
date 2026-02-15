using System.Net;
using Perplexity.Chat.Dtos;
using Perplexity.Exceptions;

namespace Perplexity.Tests.Chat.PerplexityChatClient;

public class PerplexityChatClientGetAsyncChatCompletionsTests : PerplexityChatClientTestsBase
{
    [Fact]
    public async Task GetAsyncChatCompletions_WithOnlyRequiredFields_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var asyncChatCompletions = await ChatClient.ListAsyncChatCompletions();
        var @params = new GetAsyncChatCompletionParams
        {
            ApiRequest = asyncChatCompletions.Requests.Last().Id
        };

        // act
        var response = await ChatClient.GetAsyncChatCompletion(@params);

        // assert
        ValidateValidResponse(response);
    }

    [Fact]
    public async Task GetAsyncChatCompletions_WithNonexistentChatCompetition_ThrowsException()
    {
        // arrange
        var @params = new GetAsyncChatCompletionParams
        {
            ApiRequest = "Nonexistent chat competition"
        };

        // act
        var getAsyncChatCompletion = async () => await ChatClient.GetAsyncChatCompletion(@params);

        // assert
        var exception = await Assert.ThrowsAsync<PerplexityClientException>(getAsyncChatCompletion);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        Assert.NotNull(exception.Content);
        Assert.NotNull(exception.Error);
        Assert.Equal(404, exception.Error.Code);
        Assert.NotEmpty(exception.Error.Type);
        Assert.NotEmpty(exception.Error.Message);
    }

    private static void ValidateValidResponse(GetAsyncChatCompletionResponse response)
    {
        Assert.NotNull(response);
        Assert.NotNull(response.Id);
        Assert.NotNull(response.CreatedAt);
        Assert.NotNull(response.Status);
        Assert.NotNull(response.Response);
        Assert.NotNull(response.Response.Choices);
        foreach (var choice in response.Response.Choices)
        {
            Assert.NotNull(choice);
            Assert.NotNull(choice.Message);
            Assert.Equal("assistant", choice.Message.Role);
        }
    }
}