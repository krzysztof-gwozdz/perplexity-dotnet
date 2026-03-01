using System.Net;
using Perplexity.Chat.Dtos;
using Perplexity.Common;

namespace Perplexity.Tests.Chat.PerplexityChatClient;

public class PerplexityChatClientCreateAsyncChatCompletionTests : PerplexityChatClientTestsBase
{
    private const string Model = "sonar-deep-research";
    private const string ReasoningModel = "sonar-reasoning-pro";

    [Fact]
    public async Task CreateAsyncChatCompletion_WithOnlyRequiredFields_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateAsyncChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateSystemMessage("Response with pong for ping request"),
                Message.CreateUserMessage("ping")
            ]
        };

        // act
        var result = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        ValidateSuccessfulResult(result);
    }

    [Fact]
    public async Task CreateAsyncChatCompletion_WithSamplingParameters_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateAsyncChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateSystemMessage("Response with pong for ping request"),
                Message.CreateUserMessage("ping")
            ],
            MaxTokens = 500,
            Temperature = 0.7,
            TopP = 0.9,
            Stop = ["END"]
        };

        // act
        var result = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        ValidateSuccessfulResult(result);
    }

    [Fact]
    public async Task CreateAsyncChatCompletion_WithSearchDomainFilter_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateAsyncChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateUserMessage("What is the capital of France?")
            ],
            SearchDomainFilter = ["wikipedia.org", "britannica.com"],
            MaxTokens = 500
        };

        // act
        var result = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        ValidateSuccessfulResult(result);
    }

    [Fact]
    public async Task CreateAsyncChatCompletion_WithSearchRecencyFilter_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateAsyncChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateUserMessage("What are the latest news in technology?")
            ],
            SearchRecencyFilter = "week",
            MaxTokens = 500
        };

        // act
        var result = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        ValidateSuccessfulResult(result);
    }

    [Fact]
    public async Task CreateAsyncChatCompletion_WithSearchDateFilter_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateAsyncChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateUserMessage("What happened in technology in 2024?")
            ],
            SearchAfterDateFilter = "01/01/2024",
            SearchBeforeDateFilter = "12/31/2024",
            LastUpdatedAfterFilter = "01/01/2024",
            LastUpdatedBeforeFilter = "12/31/2024",
            MaxTokens = 500
        };

        // act
        var result = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        ValidateSuccessfulResult(result);
    }

    [Fact]
    public async Task CreateAsyncChatCompletion_WithSearchLanguageFilter_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateAsyncChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateUserMessage("What is the Eiffel Tower?")
            ],
            SearchLanguageFilter = ["en", "fr"],
            MaxTokens = 500
        };

        // act
        var result = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        ValidateSuccessfulResult(result);
    }

    [Fact]
    public async Task CreateAsyncChatCompletion_WithDisabledSearch_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateAsyncChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateSystemMessage("You are a helpful assistant."),
                Message.CreateUserMessage("What is 2 + 2?")
            ],
            DisableSearch = true,
            MaxTokens = 500
        };

        // act
        var result = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        ValidateSuccessfulResult(result);
    }

    [Theory]
    [InlineData("web")]
    [InlineData("academic")]
    public async Task CreateAsyncChatCompletion_WithSearchMode_ReturnsValidResponseWithRequiredData(string searchMode)
    {
        // arrange
        var request = new CreateAsyncChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateUserMessage("What are the benefits of exercise?")
            ],
            SearchMode = searchMode,
            MaxTokens = 500
        };

        // act
        var result = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        ValidateSuccessfulResult(result);
    }

    [Fact]
    public async Task CreateAsyncChatCompletion_WithWebSearchOptions_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateAsyncChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateUserMessage("Tell me about recent AI developments.")
            ],
            WebSearchOptions = new WebSearchOptions
            {
                SearchContextSize = "low"
            },
            MaxTokens = 500
        };

        // act
        var result = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        ValidateSuccessfulResult(result);
    }

    [Fact]
    public async Task CreateAsyncChatCompletion_WithReturnImages_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateAsyncChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateUserMessage("Show me some famous paintings.")
            ],
            ReturnImages = true,
            MaxTokens = 500
        };

        // act
        var result = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        ValidateSuccessfulResult(result);
    }

    [Fact]
    public async Task CreateAsyncChatCompletion_WithReturnRelatedQuestions_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateAsyncChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateUserMessage("How does photosynthesis work?")
            ],
            ReturnRelatedQuestions = true,
            MaxTokens = 500
        };

        // act
        var result = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        ValidateSuccessfulResult(result);
    }

    [Fact]
    public async Task CreateAsyncChatCompletion_WithTextResponseFormat_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateAsyncChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateUserMessage("What is the capital of Germany?")
            ],
            ResponseFormat = new ResponseFormat { Type = "text" },
            MaxTokens = 500
        };

        // act
        var result = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        ValidateSuccessfulResult(result);
    }

    [Fact]
    public async Task CreateAsyncChatCompletion_WithLanguagePreference_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var request = new CreateAsyncChatCompletionRequest
        {
            Model = Model,
            Messages =
            [
                Message.CreateUserMessage("Tell me a fun fact.")
            ],
            LanguagePreference = "en",
            MaxTokens = 500
        };

        // act
        var result = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        ValidateSuccessfulResult(result);
    }

    [Theory]
    [InlineData("low")]
    [InlineData("medium")]
    [InlineData("high")]
    public async Task CreateAsyncChatCompletion_WithReasoningEffort_ReturnsValidResponseWithRequiredData(string reasoningEffort)
    {
        // arrange
        var request = new CreateAsyncChatCompletionRequest
        {
            Model = ReasoningModel,
            Messages =
            [
                Message.CreateUserMessage("What is 15 * 17?")
            ],
            ReasoningEffort = reasoningEffort,
            MaxTokens = 500
        };

        // act
        var result = await ChatClient.CreateAsyncChatCompletion(request);

        // assert
        ValidateSuccessfulResult(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateAsyncChatCompletion_WithInvalidModelData_ReturnsFailResponse(string? model)
    {
        // arrange
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
        var result = await ChatClient.CreateAsyncChatCompletion(request);

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
        Assert.Null(result.Error.Exception);
        Assert.Null(result.Data);
    }

    private void ValidateSuccessfulResult(Result<CreateAsyncChatCompletionResponse> result)
    {
        Assert.NotNull(result);
        Assert.True(result.IsSuccess, result.RawApiResponse.Content);
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
        Assert.NotNull(data.CreatedAt);
        Assert.NotNull(data.Status);
    }
}
