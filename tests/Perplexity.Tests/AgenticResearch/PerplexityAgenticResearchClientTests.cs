using System.Net;
using Perplexity.AgenticResearch.Dtos;

namespace Perplexity.Tests.AgenticResearch;

public class PerplexityAgenticResearchClientTests
{
    private const string Model = "xai/grok-4-1-fast-non-reasoning";

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
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.RawApiResponse);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Equal(HttpStatusCode.OK, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.Id);
        Assert.NotNull(result.Data.Model);
        Assert.NotNull(result.Data.CreatedAt);
        Assert.NotNull(result.Data.Object);
        Assert.NotNull(result.Data.Output);
        Assert.NotNull(result.Data.Status);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Search_WithInvalidInput_ReturnsFailResponse(string? input)
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
        Assert.NotNull(result.RawApiResponse);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Equal(HttpStatusCode.BadRequest, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.Error);
        Assert.Equal(400, result.Error.Code);
        Assert.NotEmpty(result.Error.Type);
        Assert.NotEmpty(result.Error.Message);
    }
}
