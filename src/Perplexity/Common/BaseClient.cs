using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Perplexity.Exceptions;

namespace Perplexity.Common;

public abstract class BaseClient
{
    private readonly HttpClient _httpClient;

    protected BaseClient(HttpClient httpClient, string apiKey)
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

    protected async Task<Result<TResponseDto>> Get<TResponseDto>(string requestUri, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.GetAsync(requestUri, cancellationToken: cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return await Result<TResponseDto>.Fail(response, cancellationToken);
            }
            return await Result<TResponseDto>.Success(response, cancellationToken);
        }
        catch (Exception exception)
        {
            throw new PerplexityClientException(exception);
        }
    }

    protected async Task<Result> Post<TRequestDto>(string requestUri, TRequestDto value, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(requestUri, value, _options, cancellationToken: cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return await Result.Fail(response, cancellationToken);
            }
            return await Result.Success(response, cancellationToken);
        }
        catch (Exception exception)
        {
            throw new PerplexityClientException(exception);
        }
    }

    protected async Task<Result<TResponseDto>> Post<TRequestDto, TResponseDto>(string requestUri, TRequestDto value, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(requestUri, value, _options, cancellationToken: cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return await Result<TResponseDto>.Fail(response, cancellationToken);
            }
            return await Result<TResponseDto>.Success(response, cancellationToken);
        }
        catch (Exception exception)
        {
            throw new PerplexityClientException(exception);
        }
    }
}