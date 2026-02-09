using Perplexity.AgenticResearch.Dtos;

namespace Perplexity.AgenticResearch;

public class PerplexityAgenticResearchClient(HttpClient httpClient) : BaseClient(httpClient)
{
    public async Task<AgenticResearchResponse> CreateResponse(AgenticResearchRequest request, CancellationToken cancellationToken = default)
    {
        var response = await Post<AgenticResearchRequest, AgenticResearchResponse>("v1/responses", request, cancellationToken);
        return response;
    }
}