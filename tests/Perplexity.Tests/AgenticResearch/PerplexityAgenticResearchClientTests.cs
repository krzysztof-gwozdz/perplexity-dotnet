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
}