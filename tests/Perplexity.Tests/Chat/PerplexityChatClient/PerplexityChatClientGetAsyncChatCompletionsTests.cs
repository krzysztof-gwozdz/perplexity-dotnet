using System.Net;
using Perplexity.Chat.Dtos;

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
            ApiRequest = asyncChatCompletions.Data!.Requests.Last().Id
        };

        // act
        var result = await ChatClient.GetAsyncChatCompletion(@params);

        // assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.RawApiRequest);
        Assert.Null(result.RawApiRequest.Content);
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
        Assert.NotNull(data.Response);
        Assert.NotNull(data.Response.Choices);
        foreach (var choice in data.Response.Choices)
        {
            Assert.NotNull(choice);
            Assert.NotNull(choice.Message);
            Assert.Equal("assistant", choice.Message.Role);
        }
    }

    [Fact]
    public async Task GetAsyncChatCompletions_WithNonexistentChatCompetition_ReturnsFailResponse()
    {
        // arrange
        var @params = new GetAsyncChatCompletionParams
        {
            ApiRequest = "Nonexistent chat competition"
        };

        // act
        var result = await ChatClient.GetAsyncChatCompletion(@params);

        // assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.RawApiRequest);
        Assert.NotEmpty(result.RawApiRequest.Headers);
        Assert.Null(result.RawApiRequest.Content);
        Assert.NotNull(result.RawApiResponse);
        Assert.Equal(HttpStatusCode.NotFound, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.RawApiResponse.Content);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.NotNull(result.Error);
        Assert.Equal(404, result.Error.Code);
        Assert.NotEmpty(result.Error.Type);
        Assert.NotEmpty(result.Error.Message);
    }
}
