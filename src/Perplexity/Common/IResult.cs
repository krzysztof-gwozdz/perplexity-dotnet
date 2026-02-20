namespace Perplexity.Common;

public interface IResult
{
    RawApiRequest RawApiRequest { get; }
    RawApiResponse RawApiResponse { get; }
    PerplexityApiError? Error { get; }
    bool IsSuccess { get; }
}