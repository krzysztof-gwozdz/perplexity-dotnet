using System.Net;
using Perplexity.Chat.Dtos;
using Perplexity.Common;

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
        ValidateSuccessfulResult(response);
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
        ValidateSuccessfulResult(response);
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
        ValidateSuccessfulResult(response);
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
        ValidateSuccessfulResult(response);
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
        ValidateSuccessfulResult(response);
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
                Message.CreateUserMessageWithFileUrlContent("https://morth.nic.in/sites/default/files/dd12-13_0.pdf",
                    "Dummy file.pdf")
            ]
        };

        // act
        var response = await ChatClient.CreateChatCompletion(request);

        // assert
        ValidateSuccessfulResult(response);
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
        ValidateSuccessfulResult(response);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateChatCompletion_WithInvalidModelData_ReturnsFailResponse(string? model)
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
        var result = await chatClient.CreateChatCompletion(request);

        // assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.RawApiRequest);
        Assert.NotEmpty(result.RawApiRequest.Headers);
        Assert.NotNull(result.RawApiRequest.Content);
        Assert.NotEmpty(result.RawApiRequest.Content);
        Assert.NotNull(result.RawApiResponse);
        Assert.Equal(HttpStatusCode.BadRequest, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.RawApiResponse.Content);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.NotNull(result.Error);
        Assert.Equal(400, result.Error.Code);
        Assert.NotEmpty(result.Error.Type);
        Assert.NotEmpty(result.Error.Message);
        Assert.Null(result.Data);
    }

    private void ValidateSuccessfulResult(Result<CreateChatCompletionResponse> result)
    {
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.RawApiRequest);
        Assert.NotEmpty(result.RawApiRequest.Headers);
        Assert.NotNull(result.RawApiRequest.Content);
        Assert.NotEmpty(result.RawApiRequest.Content);
        Assert.NotNull(result.RawApiResponse);
        Assert.Equal(HttpStatusCode.OK, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.RawApiResponse.Content);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Null(result.Error);
        Assert.NotNull(result.Data);
        var data = result.Data;
        Assert.NotNull(data.Id);
        Assert.NotNull(data.Id);
        Assert.Equal(Model, data.Model);
        Assert.NotNull(data.Created);
        Assert.NotNull(data.Choices);
        Assert.Single(data.Choices);
        foreach (var choice in data.Choices)
        {
            Assert.NotNull(choice);
            Assert.NotNull(choice.Message);
            Assert.Equal("assistant", choice.Message.Role);
        }
        Assert.NotNull(data.Usage);
        Assert.NotNull(data.Usage.PromptTokens);
        Assert.NotNull(data.Usage.CompletionTokens);
        Assert.NotNull(data.Usage.TotalTokens);
        Assert.NotNull(data.Usage.Cost);
        Assert.NotNull(data.Usage.Cost.InputTokensCost);
        Assert.NotNull(data.Usage.Cost.OutputTokensCost);
        Assert.NotNull(data.Usage.Cost.TotalCost);
    }
}