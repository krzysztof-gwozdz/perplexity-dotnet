using System.Net;
using Perplexity.AgenticResearch.Dtos;
using Perplexity.Common;

namespace Perplexity.Tests.AgenticResearch;

public class PerplexityAgenticResearchClientTests
{
    private const string Model = "perplexity/sonar";

    [Fact]
    public async Task CreateResponse_WithOnlyRequiredFields_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var agenticResearchClient = perplexityClient.AgenticResearchClient;
        var request = new AgenticResearchRequest
        {
            Input = "What is the capital of France?",
            Model = Model
        };

        // act
        var result = await agenticResearchClient.CreateResponse(request);

        // assert
        ValidateSuccessfulResult(result);
        ValidateOutputContainsMessage(result.Data!);
    }

    [Fact]
    public async Task CreateResponse_WithStringInput_ReturnsValidResponse()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var agenticResearchClient = perplexityClient.AgenticResearchClient;
        var request = new AgenticResearchRequest
        {
            Input = "What year was the Eiffel Tower built?",
            Model = Model
        };

        // act
        var result = await agenticResearchClient.CreateResponse(request);

        // assert
        ValidateSuccessfulResult(result);
        ValidateOutputContainsMessage(result.Data!);
    }

    [Fact]
    public async Task CreateResponse_WithInputMessageArray_ReturnsValidResponse()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var agenticResearchClient = perplexityClient.AgenticResearchClient;
        var request = new AgenticResearchRequest
        {
            Input = new InputItem[]
            {
                new InputMessage { Role = "user", Content = "What is the largest ocean on Earth?" }
            },
            Model = Model
        };

        // act
        var result = await agenticResearchClient.CreateResponse(request);

        // assert
        ValidateSuccessfulResult(result);
        ValidateOutputContainsMessage(result.Data!);
    }

    [Fact]
    public async Task CreateResponse_WithMultiTurnInputMessages_ReturnsValidResponse()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var agenticResearchClient = perplexityClient.AgenticResearchClient;
        var request = new AgenticResearchRequest
        {
            Input = new InputItem[]
            {
                new InputMessage { Role = "user", Content = "What is the speed of light?" },
                new InputMessage { Role = "assistant", Content = "The speed of light is approximately 299,792,458 meters per second." },
                new InputMessage { Role = "user", Content = "How long does it take light to travel from the Sun to Earth?" }
            },
            Model = Model
        };

        // act
        var result = await agenticResearchClient.CreateResponse(request);

        // assert
        ValidateSuccessfulResult(result);
        ValidateOutputContainsMessage(result.Data!);
    }

    [Fact]
    public async Task CreateResponse_WithInstructions_ReturnsValidResponse()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var agenticResearchClient = perplexityClient.AgenticResearchClient;
        var request = new AgenticResearchRequest
        {
            Input = "What are the benefits of exercise?",
            Model = Model,
            Instructions = "Answer in exactly one sentence."
        };

        // act
        var result = await agenticResearchClient.CreateResponse(request);

        // assert
        ValidateSuccessfulResult(result);
        ValidateOutputContainsMessage(result.Data!);
    }

    [Fact]
    public async Task CreateResponse_WithMaxOutputTokens_ReturnsValidResponse()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var agenticResearchClient = perplexityClient.AgenticResearchClient;
        var request = new AgenticResearchRequest
        {
            Input = "Explain quantum computing",
            Model = Model,
            MaxOutputTokens = 200
        };

        // act
        var result = await agenticResearchClient.CreateResponse(request);

        // assert
        ValidateSuccessfulResult(result);
        ValidateOutputContainsMessage(result.Data!);
    }

    [Fact]
    public async Task CreateResponse_WithWebSearchTool_ReturnsValidResponseWithSearchResults()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var agenticResearchClient = perplexityClient.AgenticResearchClient;
        var request = new AgenticResearchRequest
        {
            Input = "What are the latest developments in fusion energy?",
            Model = Model,
            Tools =
            [
                new WebSearchTool
                {
                    Filters = new WebSearchFilters
                    {
                        DomainFilter = ["wikipedia.org"]
                    }
                }
            ]
        };

        // act
        var result = await agenticResearchClient.CreateResponse(request);

        // assert
        ValidateSuccessfulResult(result);
        ValidateOutputContainsMessage(result.Data!);

        var searchResultsOutput = result.Data!.Output.OfType<SearchResultsOutputItem>().FirstOrDefault();
        if (searchResultsOutput != null)
        {
            Assert.NotNull(searchResultsOutput.Results);
            foreach (var searchResult in searchResultsOutput.Results)
            {
                Assert.NotNull(searchResult.Url);
                Assert.NotNull(searchResult.Title);
            }
        }
    }

    [Fact]
    public async Task CreateResponse_WithWebSearchToolAndUserLocation_ReturnsValidResponse()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var agenticResearchClient = perplexityClient.AgenticResearchClient;
        var request = new AgenticResearchRequest
        {
            Input = "What is the weather like today?",
            Model = Model,
            Tools =
            [
                new WebSearchTool
                {
                    UserLocation = new ToolUserLocation
                    {
                        Country = "US",
                        City = "San Francisco",
                        Region = "California"
                    }
                }
            ]
        };

        // act
        var result = await agenticResearchClient.CreateResponse(request);

        // assert
        ValidateSuccessfulResult(result);
        ValidateOutputContainsMessage(result.Data!);
    }

    [Fact]
    public async Task CreateResponse_WithAllOptionalFields_ReturnsValidResponse()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var agenticResearchClient = perplexityClient.AgenticResearchClient;
        var request = new AgenticResearchRequest
        {
            Input = "What is the population of Tokyo?",
            Model = Model,
            Instructions = "Be concise.",
            MaxOutputTokens = 200,
            MaxSteps = 2,
            Tools =
            [
                new WebSearchTool
                {
                    MaxTokens = 200,
                    Filters = new WebSearchFilters
                    {
                        DomainFilter = ["wikipedia.org"]
                    },
                    UserLocation = new ToolUserLocation
                    {
                        Country = "US"
                    }
                }
            ]
        };

        // act
        var result = await agenticResearchClient.CreateResponse(request);

        // assert
        ValidateSuccessfulResult(result);
        ValidateOutputContainsMessage(result.Data!);
        Assert.NotNull(result.Data!.Usage);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateResponse_WithInvalidInput_ReturnsFailResponse(string? input)
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var agenticResearchClient = perplexityClient.AgenticResearchClient;
        var request = new AgenticResearchRequest
        {
            Input = input,
            Model = Model
        };

        // act
        var result = await agenticResearchClient.CreateResponse(request);

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

    [Fact]
    public async Task CreateResponse_WithInvalidModel_ReturnsFailResponse()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var agenticResearchClient = perplexityClient.AgenticResearchClient;
        var request = new AgenticResearchRequest
        {
            Input = "Hello",
            Model = "non-existent-model-12345"
        };

        // act
        var result = await agenticResearchClient.CreateResponse(request);

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

    private void ValidateSuccessfulResult(Result<AgenticResearchResponse> result)
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
        Assert.NotNull(data.Model);
        Assert.True(data.CreatedAt > 0);
        Assert.NotNull(data.Object);
        Assert.NotNull(data.Output);
        Assert.NotEmpty(data.Output);
        Assert.NotNull(data.Status);
    }

    private static void ValidateOutputContainsMessage(AgenticResearchResponse data)
    {
        var messageOutput = Assert.Single(data.Output, o => o is MessageOutputItem);
        var message = (MessageOutputItem)messageOutput;
        Assert.NotNull(message.Id);
        Assert.NotNull(message.Role);
        Assert.NotNull(message.Content);
        Assert.NotEmpty(message.Content);
        var textContent = Assert.Single(message.Content, contentPart => contentPart.Type == "output_text");
        Assert.NotNull(textContent.Text);
        Assert.NotEmpty(textContent.Text);
    }
}
