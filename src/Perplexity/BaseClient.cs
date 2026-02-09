using System.Net.Http.Json;
using System.Text.Json;

namespace Perplexity;

public abstract class BaseClient(HttpClient httpClient)
{
    protected async Task<TResponse> Get<TResponse>(string requestUri, CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync(requestUri, cancellationToken: cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        response.EnsureSuccessStatusCode();
        return ParseResponse<TResponse>(content);
    }
    
    protected async Task Post<TRequest>(string requestUri, TRequest value, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsJsonAsync(requestUri, value, cancellationToken: cancellationToken);
        response.EnsureSuccessStatusCode();
    }
    
    protected async Task<TResponse> Post<TRequest, TResponse>(string requestUri, TRequest value, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsJsonAsync(requestUri, value, cancellationToken: cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        response.EnsureSuccessStatusCode();
        return ParseResponse<TResponse>(content);
    }

    private static TResponse ParseResponse<TResponse>(string content) =>
        JsonSerializer.Deserialize<TResponse>(content)
        ?? throw new JsonException($"Failed to deserialize response to {typeof(TResponse).Name}");
}