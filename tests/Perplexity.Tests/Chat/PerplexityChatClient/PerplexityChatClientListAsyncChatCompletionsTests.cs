namespace Perplexity.Tests.Chat.PerplexityChatClient;

public class PerplexityChatClientListAsyncChatCompletionsTests : PerplexityChatClientTestsBase
{
    [Fact]
    public async Task ListAsyncChatCompletions_WithOnlyRequiredFields_ReturnsValidResponseWithRequiredData()
    {
        // act
        var response = await ChatClient.ListAsyncChatCompletions();

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
}