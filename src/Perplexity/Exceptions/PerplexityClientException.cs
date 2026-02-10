using System.Net;
using System.Net.Http.Headers;
using Perplexity.Common.Dtos;

namespace Perplexity.Exceptions;

public class PerplexityClientException : Exception
{
    public HttpStatusCode? StatusCode { get; init; }
    public Dictionary<string, IEnumerable<string>> Headers { get; init; }
    public string Content { get; init; }
    public Error? Error { get; init; }
    
    public PerplexityClientException(HttpStatusCode statusCode, HttpResponseHeaders headers, string content)
    {
        StatusCode = statusCode;
        Headers = headers.ToDictionary();
        Content = content;
    }

    public PerplexityClientException(HttpStatusCode statusCode, HttpResponseHeaders headers, string content, ErrorResponse errorResponse)
    {
        StatusCode = statusCode;
        Headers = headers.ToDictionary();
        Content = content;
        Error = errorResponse.Error;
    }
}