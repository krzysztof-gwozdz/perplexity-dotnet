using Perplexity.AgenticResearch;
using Perplexity.Authentication;
using Perplexity.Chat;
using Perplexity.Embeddings;
using Perplexity.Search;

namespace Perplexity;

public interface IPerplexityClient
{
    IPerplexityAgenticResearchClient AgenticResearchClient { get; }
    IPerplexityAuthenticationClient AuthenticationClient { get; }
    IPerplexityChatClient ChatClient { get; }
    IPerplexityEmbeddingsClient EmbeddingsClient { get; }
    IPerplexitySearchClient SearchClient { get; }
}