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
        Assert.NotNull(result.RawApiResponse);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Equal(HttpStatusCode.OK, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.Requests);
        Assert.NotEmpty(result.Data.Requests);
        var request = result.Data.Requests[0];
        Assert.NotNull(request);
        Assert.NotNull(request.Id);
        Assert.NotNull(request.CreatedAt);
        Assert.NotNull(request.Status);
    }
}
