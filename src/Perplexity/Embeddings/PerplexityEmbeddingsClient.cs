using Perplexity.Common;
using Perplexity.Embeddings.Dtos;

namespace Perplexity.Embeddings;

public class PerplexityEmbeddingsClient(HttpClient httpClient, string apiKey) : BaseClient(httpClient, apiKey), IPerplexityEmbeddingsClient
{
    public async Task<Result<EmbeddingsResponse>> CreateEmbeddings(EmbeddingsRequest request, CancellationToken cancellationToken = default) =>
        await Post<EmbeddingsRequest, EmbeddingsResponse>("/v1/embeddings", request, cancellationToken);

    public async Task<Result<ContextualizedEmbeddingsResponse>> CreateContextualizedEmbeddings(ContextualizedEmbeddingsRequest request, CancellationToken cancellationToken = default) =>
        await Post<ContextualizedEmbeddingsRequest, ContextualizedEmbeddingsResponse>("/v1/contextualizedembeddings", request, cancellationToken);
}
