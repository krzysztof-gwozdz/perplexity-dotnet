using System.Net;
using Perplexity.Embeddings;
using Perplexity.Embeddings.Dtos;

namespace Perplexity.Tests.Embeddings;

public class PerplexityEmbeddingsClientTests
{
    [Fact]
    public async Task CreateEmbeddings_WithSingleInput_ReturnsSuccessWithEmbeddingData()
    {
        var perplexityClient = new PerplexityClient();
        var embeddingsClient = perplexityClient.EmbeddingsClient;
        var request = new EmbeddingsRequest
        {
            Input = ["hello world"],
            Model = EmbeddingModels.PplxEmbedV1_0_6b
        };

        var result = await embeddingsClient.CreateEmbeddings(request);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.RawApiRequest);
        Assert.NotEmpty(result.RawApiRequest.Headers);
        Assert.NotNull(result.RawApiRequest.Content);
        Assert.NotEmpty(result.RawApiRequest.Content);
        Assert.NotNull(result.RawApiResponse);
        Assert.Equal(HttpStatusCode.OK, result.RawApiResponse.StatusCode);
        Assert.NotEmpty(result.RawApiResponse.Headers);
        Assert.NotNull(result.RawApiResponse.Content);
        Assert.NotEmpty(result.RawApiResponse.Content);
        Assert.Null(result.Error);
        Assert.NotNull(result.Data);
        var data = result.Data;
        Assert.Equal("list", data.Object);
        Assert.NotNull(data.Data);
        Assert.NotEmpty(data.Data);
        Assert.NotNull(data.Model);
        Assert.NotEmpty(data.Model);
        Assert.NotNull(data.Usage);
        foreach (var item in data.Data)
        {
            Assert.Equal("embedding", item.Object);
            Assert.NotNull(item.Embedding);
            Assert.NotEmpty(item.Embedding);
            var decoded = EmbeddingBase64.DecodeInt8(item.Embedding);
            Assert.NotEmpty(decoded);
        }
    }

    [Fact]
    public async Task CreateEmbeddings_WithDimensions_ReturnsSuccessWithEmbeddingData()
    {
        var perplexityClient = new PerplexityClient();
        var embeddingsClient = perplexityClient.EmbeddingsClient;
        const int dimensions = 256;
        var request = new EmbeddingsRequest
        {
            Input = ["dimension test"],
            Model = EmbeddingModels.PplxEmbedV1_0_6b,
            Dimensions = dimensions
        };

        var result = await embeddingsClient.CreateEmbeddings(request);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.Data);
        Assert.NotEmpty(result.Data.Data);
        var decoded = EmbeddingBase64.DecodeInt8(result.Data.Data[0].Embedding);
        Assert.Equal(dimensions, decoded.Length);
    }

    [Fact]
    public async Task CreateContextualizedEmbeddings_WithOneDocumentMultipleChunks_ReturnsSuccessWithNestedEmbeddingData()
    {
        var perplexityClient = new PerplexityClient();
        var embeddingsClient = perplexityClient.EmbeddingsClient;
        var request = new ContextualizedEmbeddingsRequest
        {
            Input =
            [
                ["intro paragraph", "body paragraph"]
            ],
            Model = ContextualizedEmbeddingModels.PplxEmbedContextV1_0_6b
        };

        var result = await embeddingsClient.CreateContextualizedEmbeddings(request);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.RawApiResponse);
        Assert.Equal(HttpStatusCode.OK, result.RawApiResponse.StatusCode);
        Assert.Null(result.Error);
        Assert.NotNull(result.Data);
        var data = result.Data;
        Assert.Equal("list", data.Object);
        Assert.NotNull(data.Data);
        Assert.NotEmpty(data.Data);
        Assert.NotNull(data.Model);
        Assert.NotEmpty(data.Model);
        Assert.NotNull(data.Usage);
        foreach (var doc in data.Data)
        {
            Assert.NotNull(doc.Data);
            Assert.NotEmpty(doc.Data);
            foreach (var chunk in doc.Data)
            {
                Assert.Equal("embedding", chunk.Object);
                Assert.NotNull(chunk.Embedding);
                Assert.NotEmpty(chunk.Embedding);
                var decoded = EmbeddingBase64.DecodeInt8(chunk.Embedding);
                Assert.NotEmpty(decoded);
            }
        }
    }

    [Fact]
    public async Task CreateContextualizedEmbeddings_WithDimensions_ReturnsVectorsOfRequestedSize()
    {
        var perplexityClient = new PerplexityClient();
        var embeddingsClient = perplexityClient.EmbeddingsClient;
        const int dimensions = 256;
        var request = new ContextualizedEmbeddingsRequest
        {
            Input = [["single chunk"]],
            Model = ContextualizedEmbeddingModels.PplxEmbedContextV1_0_6b,
            Dimensions = dimensions
        };

        var result = await embeddingsClient.CreateContextualizedEmbeddings(request);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data?.Data);
        Assert.NotEmpty(result.Data.Data);
        var firstChunk = result.Data.Data[0].Data[0];
        var decoded = EmbeddingBase64.DecodeInt8(firstChunk.Embedding);
        Assert.Equal(dimensions, decoded.Length);
    }
}
