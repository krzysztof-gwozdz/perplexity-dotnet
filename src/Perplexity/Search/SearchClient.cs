using Perplexity.Search.Dtos;

namespace Perplexity.Search;

public class PerplexitySearchClient(HttpClient httpClient) : BaseClient(httpClient)
{
    private const string Url = "/search";

    public async Task<SearchResponse> Search(SearchRequest request, CancellationToken cancellationToken = default)
    {
        var response = await Post<SearchRequest, SearchResponse>(Url, request, cancellationToken);
        return response;
    }
}