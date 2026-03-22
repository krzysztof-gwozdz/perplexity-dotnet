namespace Perplexity.Embeddings;

/// <summary>
/// Decodes embedding vectors returned with <c>encoding_format</c> <c>base64_int8</c> (default).
/// </summary>
public static class EmbeddingBase64
{
    /// <summary>
    /// Decodes a base64 payload to signed int8 values (one byte per dimension).
    /// </summary>
    public static sbyte[] DecodeInt8(string base64)
    {
        if (base64 is null)
        {
            throw new ArgumentNullException(nameof(base64));
        }

        var bytes = Convert.FromBase64String(base64);
        var result = new sbyte[bytes.Length];
        for (var i = 0; i < bytes.Length; i++)
        {
            result[i] = unchecked((sbyte)bytes[i]);
        }

        return result;
    }
}
