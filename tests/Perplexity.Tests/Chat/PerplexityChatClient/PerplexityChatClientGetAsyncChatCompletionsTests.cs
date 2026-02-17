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
            ApiRequest = asyncChatCompletions.Data.Requests.Last().Id
        };

        // act
        var response = await ChatClient.GetAsyncChatCompletion(@params);

        // assert
        ValidateSuccessfulResult(response);
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
        Assert.NotNull(result.RawApiResponse);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Equal(HttpStatusCode.NotFound, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.Error);
        Assert.Equal(404, result.Error.Code);
        Assert.NotEmpty(result.Error.Type);
        Assert.NotEmpty(result.Error.Message);
    }

    private static void ValidateSuccessfulResult(Result<GetAsyncChatCompletionResponse> result)
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
        Assert.NotNull(result.Data.Response);
        Assert.NotNull(result.Data.Response.Choices);
        foreach (var choice in result.Data.Response.Choices)
        {
            Assert.NotNull(choice);
            Assert.NotNull(choice.Message);
            Assert.Equal("assistant", choice.Message.Role);
        }
    }
}
