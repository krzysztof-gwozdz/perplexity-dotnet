using Perplexity.Chat;

namespace Perplexity;

public class PerplexityClient
{
    private readonly HttpClient _httpClient;

    public PerplexityClient(string apiKey)
    {
        // todo: it's temporary solution
        _httpClient = new HttpClient(); 
        _httpClient.BaseAddress = new("https://api.perplexity.ai");
        _httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
        _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", apiKey);
    }
    
    public PerplexityChatClient ChatClient => new(_httpClient);
}