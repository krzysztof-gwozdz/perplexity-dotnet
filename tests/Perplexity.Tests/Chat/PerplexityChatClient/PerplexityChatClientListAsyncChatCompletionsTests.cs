using System.Net;

namespace Perplexity.Tests.Chat.PerplexityChatClient;

public class PerplexityChatClientListAsyncChatCompletionsTests : PerplexityChatClientTestsBase
{
    [Fact]
    public async Task ListAsyncChatCompletions_WithOnlyRequiredFields_ReturnsValidResponseWithRequiredData()
    {
        // act
        var result = await ChatClient.ListAsyncChatCompletions();

        // assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.RawApiRequest);
        Assert.NotEmpty(result.RawApiRequest.Headers);
        Assert.Null(result.RawApiRequest.Content);
        Assert.NotNull(result.RawApiResponse);
        Assert.Equal(HttpStatusCode.OK, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.RawApiResponse.Content);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Null(result.Error);
        Assert.NotNull(result.Data);
        var data = result.Data;
        Assert.NotNull(data.Requests);
        Assert.NotEmpty(data.Requests);
        foreach (var request in data.Requests)
        {
            Assert.NotNull(request);
            Assert.NotNull(request.Id);
            Assert.NotNull(request.CreatedAt);
            Assert.NotNull(request.Status);
        }
    }
}
