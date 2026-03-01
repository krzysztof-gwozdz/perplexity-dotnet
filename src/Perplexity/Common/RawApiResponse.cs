using System.Net;

namespace Perplexity.Common;

public record RawApiResponse(HttpStatusCode StatusCode, Dictionary<string, IEnumerable<string>> Headers, string Content)
{
    public static async Task<RawApiResponse> Create(HttpResponseMessage response, CancellationToken cancellationToken) =>
        new(response.StatusCode, response.Headers.ToDictionary(h => h.Key, h => h.Value),
            await response.Content.ReadAsStringAsync());

    public static RawApiResponse Create(HttpResponseMessage response, string content) =>
        new(response.StatusCode, response.Headers.ToDictionary(h => h.Key, h => h.Value), content);
}