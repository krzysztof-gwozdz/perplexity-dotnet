using Perplexity.AgenticResearch.Dtos;
using Perplexity.Common;

namespace Perplexity.AgenticResearch;

public class PerplexityAgenticResearchClient(HttpClient httpClient, string apiKey) : BaseClient(httpClient, apiKey)
{
    public async Task<AgenticResearchResponse> CreateResponse(AgenticResearchRequest request, CancellationToken cancellationToken = default)
    {
        var response = await Post<AgenticResearchRequest, AgenticResearchResponse>("v1/responses", request, cancellationToken);
        return response;
    }
}