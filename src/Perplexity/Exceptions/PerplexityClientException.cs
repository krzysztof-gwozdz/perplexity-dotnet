using System.Net;
using System.Net.Http.Headers;

namespace Perplexity.Exceptions;

public class PerplexityClientException : Exception
{
    public HttpStatusCode? StatusCode { get; init; }
    public Dictionary<string, IEnumerable<string>> Headers { get; init; }
    public string Content { get; init; }
    
    public PerplexityClientException(HttpStatusCode statusCode, HttpResponseHeaders headers, string content)
    {
        StatusCode = statusCode;
        Headers = headers.ToDictionary();
        Content = content;
    }
}