using System.Net;

namespace Perplexity;

public class Result<TDto> : Result
{
    public TDto? Data { get; }

    private Result(RawApiResponse rawApiResponse, PerplexityApiError? error, TDto? data) : base(rawApiResponse, error)
    {
        Data = data;
    }

    public static Result<TDto> Success(RawApiResponse rawApiResponse, TDto? data) =>
        new(rawApiResponse, null, data);
    
    public new static Result<TDto> Fail(RawApiResponse rawApiResponse, PerplexityApiError error) =>
        new(rawApiResponse, error, default);
}

public class Result
{
    public RawApiResponse RawApiResponse { get; }
    public PerplexityApiError? Error { get; }
    public bool IsSuccess => Error is null;

    protected Result(RawApiResponse rawApiResponse, PerplexityApiError? error)
    {
        RawApiResponse = rawApiResponse;
        Error = error;
    }
    
    public static Result Success(RawApiResponse rawApiResponse) =>
        new(rawApiResponse, null);
    
    public static Result Fail(RawApiResponse rawApiResponse, PerplexityApiError error) =>
        new(rawApiResponse, error);
}

public record PerplexityApiError(int Code, string Type, string Message);

public record RawApiResponse(
    string Content,
    HttpStatusCode StatusCode,
    Dictionary<string, IEnumerable<string>> Headers);