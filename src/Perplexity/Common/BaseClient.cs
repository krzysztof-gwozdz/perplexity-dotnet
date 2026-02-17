using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Perplexity.Common.Dtos;

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
        var response = await _httpClient.GetAsync(requestUri, cancellationToken: cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return await ParseFailResponse<TResponseDto>(response, cancellationToken);
        }
        return await ParseSuccessResponse<TResponseDto>(response, cancellationToken);
    }

    protected async Task<Result> Post<TRequestDto>(string requestUri, TRequestDto value, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync(requestUri, value, _options, cancellationToken: cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return await ParseFailResponse(response, cancellationToken);
        }
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return new Result
        {
            RawApiResponse = new RawApiResponse
            {
                Content = content,
                StatusCode = response.StatusCode,
                Headers = response.Headers.ToDictionary()
            }
        };
    }

    protected async Task<Result<TResponseDto>> Post<TRequestDto, TResponseDto>(string requestUri, TRequestDto value, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync(requestUri, value, _options, cancellationToken: cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return await ParseFailResponse<TResponseDto>(response, cancellationToken);
        }
        return await ParseSuccessResponse<TResponseDto>(response, cancellationToken);
    }

    private static async Task<Result<TResponseDto>> ParseSuccessResponse<TResponseDto>(HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var dto = JsonSerializer.Deserialize<TResponseDto>(content) ?? throw new JsonException($"Failed to deserialize response to {typeof(TResponseDto).Name}");
        return new Result<TResponseDto>
        {
            Data = dto,
            RawApiResponse = new RawApiResponse
            {
                Content = content,
                StatusCode = response.StatusCode,
                Headers = response.Headers.ToDictionary()
            }
        };
    }

    private static async Task<Result<TResponseDto>> ParseFailResponse<TResponseDto>(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        try
        {
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content) ?? throw new JsonException($"Failed to deserialize response to {nameof(ErrorResponse)}");
            return new Result<TResponseDto>
            {
                Error = new PerplexityApiError
                {
                    Code = errorResponse.Error.Code,
                    Type = errorResponse.Error.Type,
                    Message = errorResponse.Error.Message
                },
                RawApiResponse = new RawApiResponse
                {
                    Content = content,
                    StatusCode = response.StatusCode,
                    Headers = response.Headers.ToDictionary()
                }
            };
        }
        catch (Exception ex)
        {
            return new Result<TResponseDto>
            {
                Error = new PerplexityApiError
                {
                    Code = -1,
                    Type = "internal_server_error",
                    Message = ex.Message
                },
                RawApiResponse = new RawApiResponse
                {
                    Content = content,
                    StatusCode = response.StatusCode,
                    Headers = response.Headers.ToDictionary()
                }
            };
        }
    }

    private static async Task<Result> ParseFailResponse(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        try
        {
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content) ?? throw new JsonException($"Failed to deserialize response to {nameof(ErrorResponse)}");
            return new Result
            {
                Error = new PerplexityApiError
                {
                    Code = errorResponse.Error.Code,
                    Type = errorResponse.Error.Type,
                    Message = errorResponse.Error.Message
                },
                RawApiResponse = new RawApiResponse
                {
                    Content = content,
                    StatusCode = response.StatusCode,
                    Headers = response.Headers.ToDictionary()
                }
            };
        }
        catch (Exception ex)
        {
            return new Result
            {
                Error = new PerplexityApiError
                {
                    Code = -1,
                    Type = "internal_server_error",
                    Message = ex.Message
                },
                RawApiResponse = new RawApiResponse
                {
                    Content = content,
                    StatusCode = response.StatusCode,
                    Headers = response.Headers.ToDictionary()
                }
            };
        }
    }
}