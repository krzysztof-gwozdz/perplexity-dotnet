using Perplexity.AgenticResearch.Dtos;
using Perplexity.Common;

namespace Perplexity.AgenticResearch;

public class PerplexityAgenticResearchClient(HttpClient httpClient, string apiKey) : BaseClient(httpClient, apiKey), IPerplexityAgenticResearchClient
{
    public async Task<Result<AgenticResearchResponse>> CreateResponse(AgenticResearchRequest request, CancellationToken cancellationToken = default) =>
        await Post<AgenticResearchRequest, AgenticResearchResponse>("v1/responses", request, cancellationToken);
}