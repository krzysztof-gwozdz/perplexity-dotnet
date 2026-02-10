using Perplexity.AgenticResearch;
using Perplexity.Authentication;
using Perplexity.Chat;
using Perplexity.Search;

namespace Perplexity;

public class PerplexityClient
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
    
    public PerplexityAgenticResearchClient AgenticResearchClient => new(_httpClient, _apiKey);

    public PerplexityAuthenticationClient AuthenticationClient => new(_httpClient, _apiKey);

    public PerplexityChatClient ChatClient => new(_httpClient, _apiKey);
    
    public PerplexitySearchClient SearchClient => new(_httpClient, _apiKey);
}