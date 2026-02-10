using System.Net;
using Perplexity.Exceptions;
using Perplexity.Search.Dtos;

namespace Perplexity.Tests.Search;

public class PerplexitySearchClientTests
{
    private readonly string _apiKey = Environment.GetEnvironmentVariable("PERPLEXITY_APIKEY")
                                      ?? throw new PerplexityMissingApiKeyException();

    [Fact]
    public async Task Search_WithOnlyRequiredFields_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var searchClient = perplexityClient.SearchClient;
        var request = new SearchRequest { Query = "Funny cat images" };

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
    
    [Fact]
    public async Task Search_WithAllFieldsAndDateFilter_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var searchClient = perplexityClient.SearchClient;
        var request = new SearchRequest
        {
            Query = "Best programming languages for web development?",
            MaxTokens = 600,
            MaxTokensPerPage = 100,
            MaxResults = 5,
            SearchDomainFilter = ["wikipedia.org", "google.com"],
            SearchLanguageFilter = ["en", "fr"],
            SearchAfterDateFilter = "01/01/2025",
            SearchBeforeDateFilter = "12/31/2025",
            LastUpdatedAfterFilter = "01/01/2025",
            LastUpdatedBeforeFilter = "12/31/2025",
            Country = "US",
            DisplayServerTime = true,
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
        Assert.NotNull(result.Date);
        Assert.NotNull(result.LastUpdated);
        Assert.NotNull(response.ServerTime);
    }

    [Fact]
    public async Task Search_WithAllFieldsAndSearchRecencyFilter_ReturnsValidResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var searchClient = perplexityClient.SearchClient;
        var request = new SearchRequest
        {
            Query = "Best programming languages for web development?",
            MaxTokens = 100,
            MaxTokensPerPage = 100,
            MaxResults = 1,
            SearchRecencyFilter = "year"
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
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Search_WithInvalidQuery_ThrowsException(string? query)
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var searchClient = perplexityClient.SearchClient;
        var request = new SearchRequest { Query = query };

        // act
        var search = async () => await searchClient.Search(request);

        // assert
        var exception = await Assert.ThrowsAsync<PerplexityClientException>(search);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        Assert.NotNull(exception.Content);
        Assert.NotNull(exception.Error);
        Assert.Equal(400, exception.Error.Code);
        Assert.NotEmpty(exception.Error.Type);
        Assert.NotEmpty(exception.Error.Message);
    }

    [Fact]
    public async Task Search_WithWhitespaceQuery_ThrowsException()
    {
        // arrange
        var perplexityClient = new PerplexityClient(_apiKey);
        var searchClient = perplexityClient.SearchClient;
        var request = new SearchRequest { Query = " " };

        // act
        var search = async () => await searchClient.Search(request);

        // assert
        var exception = await Assert.ThrowsAsync<PerplexityClientException>(search);
        Assert.Equal(HttpStatusCode.GatewayTimeout, exception.StatusCode); // Yes, that looks like a bug.
        Assert.Equal("error code: 504", exception.Content);
    }
}