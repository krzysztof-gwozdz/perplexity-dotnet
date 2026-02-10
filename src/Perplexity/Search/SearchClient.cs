using Perplexity.Common;
using Perplexity.Search.Dtos;

namespace Perplexity.Search;

public class PerplexitySearchClient(HttpClient httpClient, string apiKey) : BaseClient(httpClient, apiKey)
{
    public async Task<SearchResponse> Search(SearchRequest request, CancellationToken cancellationToken = default)
    {
        var response = await Post<SearchRequest, SearchResponse>("/search", request, cancellationToken);
        return response;
    }
}