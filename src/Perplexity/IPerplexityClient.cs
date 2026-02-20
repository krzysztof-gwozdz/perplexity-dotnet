using Perplexity.AgenticResearch;
using Perplexity.Authentication;
using Perplexity.Chat;
using Perplexity.Search;

namespace Perplexity;

public interface IPerplexityClient
{
    IPerplexityAgenticResearchClient AgenticResearchClient { get; }
    IPerplexityAuthenticationClient AuthenticationClient { get; }
    IPerplexityChatClient ChatClient { get; }
    IPerplexitySearchClient SearchClient { get; }
}