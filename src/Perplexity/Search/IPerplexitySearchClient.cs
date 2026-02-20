using Perplexity.Common;
using Perplexity.Search.Dtos;

namespace Perplexity.Search;

public interface IPerplexitySearchClient
{
    Task<Result<SearchResponse>> Search(SearchRequest request, CancellationToken cancellationToken = default);
}