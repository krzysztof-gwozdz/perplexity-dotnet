using Perplexity.AgenticResearch;
using Perplexity.Authentication;
using Perplexity.Chat;
using Perplexity.Search;

namespace Perplexity;

public class PerplexityClient : IPerplexityClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    
    public PerplexityClient() : this(PerplexityMissingApiKey.Value)
    {
    }
    
    public PerplexityClient(string apiKey) : this(new HttpClient(), apiKey)
    {
    }
    
    public PerplexityClient(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }
    
    public IPerplexityAgenticResearchClient AgenticResearchClient => new PerplexityAgenticResearchClient(_httpClient, _apiKey);

    public IPerplexityAuthenticationClient AuthenticationClient => new PerplexityAuthenticationClient(_httpClient, _apiKey);

    public IPerplexityChatClient ChatClient => new PerplexityChatClient(_httpClient, _apiKey);
    
    public IPerplexitySearchClient SearchClient => new PerplexitySearchClient(_httpClient, _apiKey);
}