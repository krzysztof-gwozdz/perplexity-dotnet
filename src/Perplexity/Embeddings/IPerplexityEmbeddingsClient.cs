using Perplexity.Common;
using Perplexity.Embeddings.Dtos;

namespace Perplexity.Embeddings;

public interface IPerplexityEmbeddingsClient
{
    Task<Result<EmbeddingsResponse>> CreateEmbeddings(EmbeddingsRequest request, CancellationToken cancellationToken = default);

    Task<Result<ContextualizedEmbeddingsResponse>> CreateContextualizedEmbeddings(ContextualizedEmbeddingsRequest request, CancellationToken cancellationToken = default);
}
