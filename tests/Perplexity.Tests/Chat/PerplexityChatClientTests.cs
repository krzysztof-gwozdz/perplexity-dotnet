using Perplexity.Chat.Dtos;
using Perplexity.Exceptions;

namespace Perplexity.Tests.Chat;

public class PerplexityChatClientTests
{
    private readonly string _apiKey = Environment.GetEnvironmentVariable("PERPLEXITY_APIKEY")
                                      ?? throw new PerplexityMissingApiKeyException();

    [Fact]
    public async Task CompleteChat_WithOnltRequiredData_ReturnsValidResponse()
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var chatClient = perplexityClient.ChatClient;
        var request = new CreateChatCompletionRequest
        {
            Model = "sonar",
            Messages =
            [
                new ChatCompletionMessage
                {
                    Role = "user",
                    Content = "Hi"
                }
            ]
        };

        // act
        var response = await chatClient.CreateChatCompletion(request);

        // assert
        Assert.NotNull(response);
        Assert.NotNull(response.Choices);
        Assert.Single(response.Choices);
        var choice = response.Choices[0];
        Assert.NotNull(choice);
        Assert.NotNull(choice.Message);
        Assert.Equal("assistant", choice.Message.Role);
        Assert.Single(request.Messages);
        Assert.NotEmpty(request.Messages[0].Content);
    }
}