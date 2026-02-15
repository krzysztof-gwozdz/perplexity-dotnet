#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record Message
{
    [JsonPropertyName("role")]
    public string Role { get; init; }

    [JsonPropertyName("content")]
    [JsonConverter(typeof(MessageContentConverter))]
    public object Content { get; init; }
    
    public static Message CreateUserMessage(string content) => 
        new() { Role = Roles.User, Content = content };
    
    public static Message CreateUserMessage(MessageContentChunk contentChunk) => 
        new() { Role = Roles.User, Content = contentChunk };
    
    public static Message CreateUserMessage(IReadOnlyList<MessageContentChunk> contentChunks) => 
        new() { Role = Roles.User, Content = contentChunks };
    
    public static Message CreateUserMessageWithTextContent(string text) => 
        new() { Role = Roles.User, Content = new TextContentChunk { Text = text } };
        
    public static Message CreateUserMessageWithImageUrlContent(string imageUrl) => 
        new() { Role = Roles.User, Content = new ImageUrlContentChunk { ImageUrl = new UrlInfo { Url = imageUrl } } };

    public static Message CreateUserMessageWithVideoUrlContent(string videoUrl) => 
        new() { Role = Roles.User, Content = new VideoUrlContentChunk { VideoUrl = new UrlInfo { Url = videoUrl } } };

    public static Message CreateUserMessageWithFileUrlContent(string fileUrl, string fileName = null) => 
        new() { Role = Roles.User, Content = new FileUrlContentChunk { FileUrl = new UrlInfo { Url = fileUrl }, FileName = fileName } };

    public static Message CreateUserMessageWithPdfUrlContent(string pdfUrl) => 
        new() { Role = Roles.User, Content = new PdfUrlContentChunk { PdfUrl = new UrlInfo { Url = pdfUrl } } };
    
    public static Message CreateAssistantMessage(string content) => 
        new() { Role = Roles.Assistant, Content = content };    
    
    public static Message CreateAssistantMessage(MessageContentChunk contentChunks) => 
        new() { Role = Roles.Assistant, Content = contentChunks };
    
    public static Message CreateAssistantMessage(IReadOnlyList<MessageContentChunk> contentChunks) => 
        new() { Role = Roles.Assistant, Content = contentChunks };

    public static Message CreateAssistantMessageWithTextContent(string text) => 
        new() { Role = Roles.Assistant, Content = new TextContentChunk { Text = text } };
        
    public static Message CreateAssistantMessageWithImageUrlContent(string imageUrl) => 
        new() { Role = Roles.Assistant, Content = new ImageUrlContentChunk { ImageUrl = new UrlInfo { Url = imageUrl } } };

    public static Message CreateAssistantMessageWithVideoUrlContent(string videoUrl) => 
        new() { Role = Roles.Assistant, Content = new VideoUrlContentChunk { VideoUrl = new UrlInfo { Url = videoUrl } } };

    public static Message CreateAssistantMessageWithFileUrlContent(string fileUrl, string fileName = null) => 
        new() { Role = Roles.Assistant, Content = new FileUrlContentChunk { FileUrl = new UrlInfo { Url = fileUrl }, FileName = fileName } };

    public static Message CreateAssistantMessageWithPdfUrlContent(string pdfUrl) => 
        new() { Role = Roles.Assistant, Content = new PdfUrlContentChunk { PdfUrl = new UrlInfo { Url = pdfUrl } } };
    
    public static Message CreateSystemMessage(string content) => 
        new() { Role = Roles.System, Content = content };
    
    public static Message CreateSystemMessage(MessageContentChunk contentChunks) => 
        new() { Role = Roles.System, Content = contentChunks };
    
    public static Message CreateSystemMessage(IReadOnlyList<MessageContentChunk> contentChunks) => 
        new() { Role = Roles.System, Content = contentChunks };

    public static Message CreateSystemMessageWithTextContent(string text) => 
        new() { Role = Roles.System, Content = new TextContentChunk { Text = text } };
        
    public static Message CreateSystemMessageWithImageUrlContent(string imageUrl) => 
        new() { Role = Roles.System, Content = new ImageUrlContentChunk { ImageUrl = new UrlInfo { Url = imageUrl } } };

    public static Message CreateSystemMessageWithVideoUrlContent(string videoUrl) => 
        new() { Role = Roles.System, Content = new VideoUrlContentChunk { VideoUrl = new UrlInfo { Url = videoUrl } } };

    public static Message CreateSystemMessageWithFileUrlContent(string fileUrl, string fileName = null) => 
        new() { Role = Roles.System, Content = new FileUrlContentChunk { FileUrl = new UrlInfo { Url = fileUrl }, FileName = fileName } };

    public static Message CreateSystemMessageWithPdfUrlContent(string pdfUrl) => 
        new() { Role = Roles.System, Content = new PdfUrlContentChunk { PdfUrl = new UrlInfo { Url = pdfUrl } } };
    
    public static Message CreateToolMessage(string content) => 
        new() { Role = Roles.Tool, Content = content };
}