namespace Perplexity.Common;

public record RawApiRequest(Dictionary<string, IEnumerable<string>> Headers, string? Content)
{
    public static async Task<RawApiRequest> Create(HttpResponseMessage response, CancellationToken cancellationToken) =>
        new(
            response.RequestMessage!.Headers.ToDictionary(),
            response.RequestMessage?.Content is null ? null : await response.RequestMessage!.Content.ReadAsStringAsync(cancellationToken)
        );
}