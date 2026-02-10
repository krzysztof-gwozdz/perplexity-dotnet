using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Perplexity.Exceptions;

namespace Perplexity;

public abstract class BaseClient(HttpClient httpClient)
{
    private readonly JsonSerializerOptions _options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };

    protected async Task<TResponse> Get<TResponse>(string requestUri, CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync(requestUri, cancellationToken: cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new PerplexityClientException(response.StatusCode, response.Headers, content);
        }
        return ParseResponse<TResponse>(content);
    }

    protected async Task Post<TRequest>(string requestUri, TRequest value, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsJsonAsync(requestUri, value, _options, cancellationToken: cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new PerplexityClientException(response.StatusCode, response.Headers, content);
        }
    }

    protected async Task<TResponse> Post<TRequest, TResponse>(string requestUri, TRequest value, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsJsonAsync(requestUri, value, _options, cancellationToken: cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new PerplexityClientException(response.StatusCode, response.Headers, content);
        }
        return ParseResponse<TResponse>(content);
    }

    private static TResponse ParseResponse<TResponse>(string content) =>
        JsonSerializer.Deserialize<TResponse>(content)
        ?? throw new JsonException($"Failed to deserialize response to {typeof(TResponse).Name}");
}