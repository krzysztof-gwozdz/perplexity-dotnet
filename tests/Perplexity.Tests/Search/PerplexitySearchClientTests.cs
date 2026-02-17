using System.Net;
using Perplexity.Search.Dtos;

namespace Perplexity.Tests.Search;

public class PerplexitySearchClientTests
{
    [Fact]
    public async Task Search_WithOnlyRequiredFields_ReturnsSuccessResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var searchClient = perplexityClient.SearchClient;
        var request = new SearchRequest { Query = "Funny cat images" };

        // act
        var result = await searchClient.Search(request);

        // assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.RawApiResponse);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Equal(HttpStatusCode.OK, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.Id);
        Assert.NotNull(result.Data.Results);
        Assert.NotEmpty(result.Data.Results);
        foreach (var searchResult in result.Data.Results)
        {
            Assert.NotNull(searchResult);
            Assert.NotNull(searchResult.Title);
            Assert.NotNull(searchResult.Url);
            Assert.NotNull(searchResult.Snippet);
        }
    }
    
    [Fact]
    public async Task Search_WithAllFieldsAndDateFilter_ReturnsSuccessResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
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
        var result = await searchClient.Search(request);

        // assert
        Assert.NotNull(result);
        Assert.NotNull(result.RawApiResponse);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Equal(HttpStatusCode.OK, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.Id);
        Assert.NotNull(result.Data.Results);
        Assert.NotEmpty(result.Data.Results);
        foreach (var searchResult in result.Data.Results)
        {
            Assert.NotNull(searchResult);
            Assert.NotNull(searchResult.Title);
            Assert.NotNull(searchResult.Url);
            Assert.NotNull(searchResult.Snippet);
            Assert.NotNull(searchResult.Date);
            Assert.NotNull(searchResult.LastUpdated);
        }
        Assert.NotNull(result.Data.ServerTime);
    }

    [Fact]
    public async Task Search_WithAllFieldsAndSearchRecencyFilter_ReturnsSuccessResponseWithRequiredData()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
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
        var result = await searchClient.Search(request);

        // assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.RawApiResponse);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Equal(HttpStatusCode.OK, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.Id);
        Assert.NotNull(result.Data.Results);
        Assert.NotEmpty(result.Data.Results);
        foreach (var searchResult in result.Data.Results)
        {
            Assert.NotNull(searchResult);
            Assert.NotNull(searchResult.Title);
            Assert.NotNull(searchResult.Url);
            Assert.NotNull(searchResult.Snippet);
        }
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Search_WithInvalidQuery_ReturnsFailResponse(string? query)
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var searchClient = perplexityClient.SearchClient;
        var request = new SearchRequest { Query = query };

        // act
        var result = await searchClient.Search(request);

        // assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.RawApiResponse);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Equal(HttpStatusCode.BadRequest, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.Error);
        Assert.Equal(400, result.Error.Code);
        Assert.NotEmpty(result.Error.Type);
        Assert.NotEmpty(result.Error.Message);
    }

    [Fact]
    public async Task Search_WithWhitespaceQuery_ReturnsFailResponse()
    {
        // arrange
        var perplexityClient = new PerplexityClient();
        var searchClient = perplexityClient.SearchClient;
        var request = new SearchRequest { Query = " " };

        // act
        var result = await searchClient.Search(request);

        // assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.RawApiResponse);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Equal(HttpStatusCode.GatewayTimeout, result.RawApiResponse.StatusCode);  // Yes, that looks like a bug.
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.Null(result.Error);
    }
}