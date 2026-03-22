using Perplexity.Embeddings;

namespace Perplexity.Tests.Embeddings;

public class EmbeddingBase64Tests
{
    [Fact]
    public void DecodeInt8_RoundTripsSignedBytes()
    {
        var bytes = new byte[] { 0, 127, 255 };
        var base64 = Convert.ToBase64String(bytes);

        var decoded = EmbeddingBase64.DecodeInt8(base64);

        Assert.Equal(3, decoded.Length);
        Assert.Equal(0, decoded[0]);
        Assert.Equal(127, decoded[1]);
        Assert.Equal(-1, decoded[2]);
    }

    [Fact]
    public void DecodeInt8_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => EmbeddingBase64.DecodeInt8(null!));
    }
}
