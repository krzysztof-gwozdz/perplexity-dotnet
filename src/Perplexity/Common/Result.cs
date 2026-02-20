using System.Text.Json;
using Perplexity.Common.Dtos;

namespace Perplexity.Common;

public class Result : IResult
{
    public RawApiRequest RawApiRequest { get; }
    public RawApiResponse RawApiResponse { get; }
    public PerplexityApiError? Error { get; }

    public bool IsSuccess => Error is null;

    private Result(RawApiRequest rawApiRequest, RawApiResponse rawApiResponse, PerplexityApiError? error)
    {
        RawApiRequest = rawApiRequest;
        RawApiResponse = rawApiResponse;
        Error = error;
    }

    public static async Task<Result> Success(HttpResponseMessage response, CancellationToken cancellationToken) =>
        new(await RawApiRequest.Create(response, cancellationToken), await RawApiResponse.Create(response, cancellationToken), null);

    public static async Task<Result> Fail(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        try
        {
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content) ?? throw new JsonException($"Failed to deserialize response to {nameof(ErrorResponse)}");
            return new
            (
                await RawApiRequest.Create(response, cancellationToken),
                RawApiResponse.Create(response, content),
                PerplexityApiError.Create(errorResponse)
            );
        }
        catch (Exception exception)
        {
            return new
            (
                await RawApiRequest.Create(response, cancellationToken),
                RawApiResponse.Create(response, content),
                PerplexityApiError.Create(exception)
            );
        }
    }
}