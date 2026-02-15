using System.Net;
using Perplexity.Chat.Dtos;
using Perplexity.Exceptions;

namespace Perplexity.Tests.Chat.PerplexityChatClient;

public class PerplexityChatClientCreateChatCompletionTests : PerplexityChatClientTestsBase
{
    private const string Model = "sonar";

    [Fact]
    public async Task CreateChatCompletion_WithTextMessage_ReturnsValidResponseWithRequiredData()
    {
        // arrange
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
        var response = await ChatClient.CreateChatCompletion(request);

        // assert
        ValidateValidResponse(response);
    }

    [Fact]
    public async Task CreateChatCompletion_WithTextContent_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateSystemMessage("Response with pong for ping request"),
                Message.CreateUserMessageWithTextContent("ping")
            ]
        };

        // act
        var response = await ChatClient.CreateChatCompletion(request);

        // assert
        ValidateValidResponse(response);
    }


    [Fact]
    public async Task CreateChatCompletion_WithManyChunksContent_ReturnsValidResponseWithRequiredData()
    {
        var request = new CreateChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateUserMessage(
                [
                    new TextContentChunk { Text = "Tell me a joke." },
                    new TextContentChunk { Text = "It should start with: There was a bar" },
                    new TextContentChunk { Text = "Should be about a duck." }
                ])
            ]
        };

        // act
        var response = await ChatClient.CreateChatCompletion(request);

        // assert
        ValidateValidResponse(response);
    }

    [Fact]
    public async Task CreateChatCompletion_WithImageUrlContent_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateSystemMessage("What do you see on the image?"),
                Message.CreateUserMessageWithImageUrlContent("https://pl.wikipedia.org/static/images/icons/wikipedia.png")
            ]
        };

        // act
        var response = await ChatClient.CreateChatCompletion(request);

        // assert
        ValidateValidResponse(response);
    }

    [Fact]
    public async Task CreateChatCompletion_WithVideoUrlContent_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateSystemMessage("What do you see on the video?"),
                Message.CreateUserMessageWithVideoUrlContent("https://www.w3schools.com/tags/mov_bbb.mp4")
            ]
        };

        // act
        var response = await ChatClient.CreateChatCompletion(request);

        // assert
        ValidateValidResponse(response);
    }

    [Fact]
    public async Task CreateChatCompletion_WithFileUrlContent_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateSystemMessage("Describe the content of this file in one sentence."),
                Message.CreateUserMessageWithFileUrlContent("https://morth.nic.in/sites/default/files/dd12-13_0.pdf", "Dummy file.pdf")
            ]
        };

        // act
        var response = await ChatClient.CreateChatCompletion(request);

        // assert
        ValidateValidResponse(response);
    }

    [Fact(Skip = "It just does not work")]
    public async Task CreateChatCompletion_WithPdfUrlContent_ReturnsValidResponseWithRequiredData()
    {
        var request = new CreateChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateSystemMessage("Describe the content of this file in."),
                Message.CreateUserMessageWithPdfUrlContent("https://morth.nic.in/sites/default/files/dd12-13_0.pdf")
            ]
        };

        // act
        var response = await ChatClient.CreateChatCompletion(request);

        // assert
        ValidateValidResponse(response);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateChatCompletion_WithInvalidModelData_ThrowsException(string? model)
    {
        // arrange
        var perplexityClient = new PerplexityClient();
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

    private static void ValidateValidResponse(CreateChatCompletionResponse response)
    {
        Assert.NotNull(response);
        Assert.NotNull(response.Id);
        Assert.Equal(Model, response.Model);
        Assert.NotNull(response.Created);
        Assert.NotNull(response.Choices);
        Assert.Single(response.Choices);
        foreach (var choice in response.Choices)
        {
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
}