using Perplexity.Exceptions;
using Perplexity.Search.Dtos;

namespace Perplexity.Tests.Search;

public class PerplexitySearchClientTests
{
    private readonly string _apiKey = Environment.GetEnvironmentVariable("PERPLEXITY_APIKEY")
                                      ?? throw new PerplexityMissingApiKeyException();

    [Fact]
    public async Task Search_WithOnlyRequiredData_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var searchClient = perplexityClient.SearchClient;
        var request = new SearchRequest
        {
            Query = "Funny cat images"
        };

        // act
        var response = await searchClient.Search(request);

        // assert
        Assert.NotNull(response);
        Assert.NotNull(response.Id);
        Assert.NotNull(response.Results);
        Assert.NotEmpty(response.Results);
        var result = response.Results[0];
        Assert.NotNull(result);
        Assert.NotNull(result.Title);
        Assert.NotNull(result.Url);
        Assert.NotNull(result.Snippet);
    }
}