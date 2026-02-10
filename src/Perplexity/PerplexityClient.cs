using Perplexity.AgenticResearch;
using Perplexity.Authentication;
using Perplexity.Chat;
using Perplexity.Search;

namespace Perplexity;

public class PerplexityClient(HttpClient httpClient, string apiKey)
{
    public PerplexityClient(string apiKey) : this(new HttpClient(), apiKey)
    {
    }
    
    public PerplexityAgenticResearchClient AgenticResearchClient => new(httpClient, apiKey);

    public PerplexityAuthenticationClient AuthenticationClient => new(httpClient, apiKey);

    public PerplexityChatClient ChatClient => new(httpClient, apiKey);
    
    public PerplexitySearchClient SearchClient => new(httpClient, apiKey);
}