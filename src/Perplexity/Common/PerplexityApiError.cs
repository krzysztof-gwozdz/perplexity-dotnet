using System.Net;
using Perplexity.Common.Dtos;

namespace Perplexity.Common;

public record PerplexityApiError(int Code, string Type, string Message, Exception? Exception)
{
    public static PerplexityApiError Create(ErrorResponse errorResponse) =>
        new(errorResponse.Error.Code, errorResponse.Error.Type, errorResponse.Error.Message, null);
    
    public static PerplexityApiError Create(HttpStatusCode code, string message, Exception exception) =>
        new((int)code, GetSnakeCaseName(code), message, exception);

    private static string GetSnakeCaseName(HttpStatusCode code) =>
        string.Concat(code.ToString().Select((c, i) => i > 0 && char.IsUpper(c) ? "_" + char.ToLowerInvariant(c) : char.ToLowerInvariant(c).ToString()));
}