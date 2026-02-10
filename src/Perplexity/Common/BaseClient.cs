using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Perplexity.Common.Dtos;
using Perplexity.Exceptions;

namespace Perplexity.Common;

public abstract class BaseClient
{
    private readonly HttpClient _httpClient;
    
    public BaseClient(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new("https://api.perplexity.ai");
        _httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
        _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", apiKey);
    }
    
    private readonly JsonSerializerOptions _options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };

    protected async Task<TResponse> Get<TResponse>(string requestUri, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(requestUri, cancellationToken: cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            ThrowOnError(response, content);
        }
        return ParseResponse<TResponse>(content);
    }

    protected async Task Post<TRequest>(string requestUri, TRequest value, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync(requestUri, value, _options, cancellationToken: cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            ThrowOnError(response, content);
        }
    }

    protected async Task<TResponse> Post<TRequest, TResponse>(string requestUri, TRequest value, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync(requestUri, value, _options, cancellationToken: cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            ThrowOnError(response, content);
        }
        return ParseResponse<TResponse>(content);
    }

    private static void ThrowOnError(HttpResponseMessage response, string content)
    {
        try
        {
            var errorResponse = ParseResponse<ErrorResponse>(content);
            throw new PerplexityClientException(response.StatusCode, response.Headers, content, errorResponse);
        }
        catch (Exception ex) when (ex is not PerplexityClientException)
        {
            throw new PerplexityClientException(response.StatusCode, response.Headers, content);
        }
    }

    private static TResponse ParseResponse<TResponse>(string content) =>
        JsonSerializer.Deserialize<TResponse>(content)
        ?? throw new JsonException($"Failed to deserialize response to {typeof(TResponse).Name}");
}