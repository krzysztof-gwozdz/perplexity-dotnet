using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Perplexity.Common.Dtos;

namespace Perplexity.Common;

public class Result<TDto> : IResult
{
    public RawApiRequest RawApiRequest { get; }
    public RawApiResponse RawApiResponse { get; }
    public PerplexityApiError? Error { get; }
    public TDto? Data { get; }

    [MemberNotNullWhen(true, nameof(Data))]
    public bool IsSuccess => Error is null && Data is not null;

    private Result(RawApiRequest rawApiRequest, RawApiResponse rawApiResponse, PerplexityApiError? error, TDto? data)
    {
        RawApiRequest = rawApiRequest;
        RawApiResponse = rawApiResponse;
        Error = error;
        Data = data;
    }

    public static async Task<Result<TDto>> Success(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var dto = JsonSerializer.Deserialize<TDto>(content) ?? throw new JsonException($"Failed to deserialize response to {typeof(TDto).Name}");
        return new(
            await RawApiRequest.Create(response, cancellationToken),
            RawApiResponse.Create(response, content),
            null,
            dto);
    }

    public static async Task<Result<TDto>> Fail(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        try
        {
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content) ?? throw new JsonException($"Failed to deserialize response to {nameof(ErrorResponse)}");
            return new
            (
                await RawApiRequest.Create(response, cancellationToken),
                RawApiResponse.Create(response, content),
                PerplexityApiError.Create(errorResponse),
                default
            );
        }
        catch (Exception exception)
        {
            return new
            (
                await RawApiRequest.Create(response, cancellationToken),
                RawApiResponse.Create(response, content),
                PerplexityApiError.Create(response.StatusCode, content, exception),
                default
            );
        }
    }
}