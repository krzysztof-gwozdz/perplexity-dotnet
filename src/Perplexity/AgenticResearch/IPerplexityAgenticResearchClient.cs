using Perplexity.AgenticResearch.Dtos;
using Perplexity.Common;

namespace Perplexity.AgenticResearch;

public interface IPerplexityAgenticResearchClient
{
    Task<Result<AgenticResearchResponse>> CreateResponse(AgenticResearchRequest request, CancellationToken cancellationToken = default);
}