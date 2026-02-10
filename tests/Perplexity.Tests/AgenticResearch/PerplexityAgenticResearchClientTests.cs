using System.Net;
using Perplexity.AgenticResearch.Dtos;
using Perplexity.Exceptions;

namespace Perplexity.Tests.AgenticResearch;

public class PerplexityAgenticResearchClientTests
{
    private const string Model = "xai/grok-4-1-fast-non-reasoning";
    private readonly string _apiKey = Environment.GetEnvironmentVariable("PERPLEXITY_APIKEY")
                                      ?? throw new PerplexityMissingApiKeyException();

    [Fact]
    public async Task CreateResponse_WithOnlyRequiredData_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var agenticResearchClient = perplexityClient.AgenticResearchClient;
        var request = new AgenticResearchRequest
        {
            Input = "What is the capital of France?",
            Model = Model
        };

        // act
        var response = await agenticResearchClient.CreateResponse(request);

        // assert
        Assert.NotNull(response);
        Assert.NotNull(response.Id);
        Assert.NotNull(response.Model);
        Assert.NotNull(response.CreatedAt);
        Assert.NotNull(response.Object);
        Assert.NotNull(response.Output);
        Assert.NotNull(response.Status);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Search_WithInvalidInput_ThrowsException(string? input)
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var agenticResearchClient = perplexityClient.AgenticResearchClient;
        var request = new AgenticResearchRequest
        {
            Input = input,
            Model = Model
        };

        // act
        var createResponse = async () => await agenticResearchClient.CreateResponse(request);

        // assert
        var exception = await Assert.ThrowsAsync<PerplexityClientException>(createResponse);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        Assert.NotNull(exception.Content);
        Assert.NotNull(exception.Error);
        Assert.Equal(400, exception.Error.Code);
        Assert.NotEmpty(exception.Error.Type);
        Assert.NotEmpty(exception.Error.Message);
    }
}