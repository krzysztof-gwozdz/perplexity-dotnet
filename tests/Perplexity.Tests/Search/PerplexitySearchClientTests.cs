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
        Assert.NotNull(data.Results);
        Assert.NotEmpty(data.Results);
        foreach (var searchResult in data.Results)
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
            DisplayServerTime = true
        };

        // act
        var result = await searchClient.Search(request);

        // assert
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
        Assert.NotNull(data.Results);
        Assert.NotEmpty(data.Results);
        foreach (var searchResult in data.Results)
        {
            Assert.NotNull(searchResult);
            Assert.NotNull(searchResult.Title);
            Assert.NotNull(searchResult.Url);
            Assert.NotNull(searchResult.Snippet);
            Assert.NotNull(searchResult.Date);
            Assert.NotNull(searchResult.LastUpdated);
        }

        Assert.NotNull(data.ServerTime);
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
        Assert.NotNull(data.Results);
        Assert.NotEmpty(data.Results);
        foreach (var searchResult in data.Results)
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
}