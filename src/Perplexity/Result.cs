using System.Net;

namespace Perplexity;

public class Result<TDto> : Result
{
    public TDto? Data { get; init; }
}

public class Result
{
    public PerplexityApiError? Error { get; init; }
    public RawApiResponse RawApiResponse { get; init; }    
    public bool IsSuccess => Error is null;         
}

public record PerplexityApiError
{
    public int Code { get; init; }
    public string Type { get; init; }
    public string Message { get; init; }
}

public record RawApiResponse
{
    public string Content { get; init; }
    
    public HttpStatusCode StatusCode { get; init; }
    
    public Dictionary<string, IEnumerable<string>> Headers { get; init; }
}