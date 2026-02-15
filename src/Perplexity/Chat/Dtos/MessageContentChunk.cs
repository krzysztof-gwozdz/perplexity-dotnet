#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(TextContentChunk), "text")]
[JsonDerivedType(typeof(ImageUrlContentChunk), "image_url")]
[JsonDerivedType(typeof(VideoUrlContentChunk), "video_url")]
[JsonDerivedType(typeof(FileUrlContentChunk), "file_url")]
[JsonDerivedType(typeof(PdfUrlContentChunk), "pdf_url")]
public abstract record MessageContentChunk;

public record TextContentChunk : MessageContentChunk
{
    [JsonPropertyName("text")]
    public string Text { get; init; }
}

public record ImageUrlContentChunk : MessageContentChunk
{
    [JsonPropertyName("image_url")]
    public UrlInfo ImageUrl { get; init; }
}

public record VideoUrlContentChunk : MessageContentChunk
{
    [JsonPropertyName("video_url")]
    public UrlInfo VideoUrl { get; init; }
}


public record FileUrlContentChunk : MessageContentChunk
{
    [JsonPropertyName("file_url")]
    public UrlInfo FileUrl { get; init; }

    [JsonPropertyName("file_name")]
    public string FileName { get; init; }
}

public record PdfUrlContentChunk : MessageContentChunk
{
    [JsonPropertyName("pdf_url")]
    public UrlInfo PdfUrl { get; init; }
}