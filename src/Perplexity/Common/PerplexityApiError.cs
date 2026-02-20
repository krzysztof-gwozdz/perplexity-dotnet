using Perplexity.Common.Dtos;

namespace Perplexity.Common;

public record PerplexityApiError(int Code, string Type, string Message)
{
    public static PerplexityApiError Create(ErrorResponse errorResponse) =>
        new(errorResponse.Error.Code, errorResponse.Error.Type, errorResponse.Error.Message);
    
    public static PerplexityApiError Create(Exception exception) =>
        new(-1, "internal_server_error", exception.Message);
}