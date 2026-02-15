using Perplexity.Chat.Dtos;

namespace Perplexity.Tests.Chat.Dtos;

public class MessageTests
{
    [Fact]
    public void CreateUserMessage_WithContent_SetsRoleAndContent()
    {
        // arrange
        const string content = "Hello";

        // act
        var message = Message.CreateUserMessage(content);

        // assert
        Assert.Equal("user", message.Role);
        Assert.Equal(content, message.Content);
    }

    [Fact]
    public void CreateUserMessage_WithContentChunk_SetsRoleAndContent()
    {
        // arrange
        var contentChunk = new TextContentChunk { Text = "Hello" };

        // act
        var message = Message.CreateUserMessage(contentChunk);

        // assert
        Assert.Equal("user", message.Role);
        Assert.Equal(contentChunk, message.Content);
    }

    [Fact]
    public void CreateUserMessage_WithContentChunks_SetsRoleAndContent()
    {
        // arrange
        var contentChunk1 = new TextContentChunk { Text = "Hello" };
        var contentChunk2 = new TextContentChunk { Text = "World" };

        // act
        var message = Message.CreateUserMessage([contentChunk1, contentChunk2]);

        // assert
        Assert.Equal("user", message.Role);
        Assert.IsType<IReadOnlyList<MessageContentChunk>>(message.Content, exactMatch: false);
        Assert.Equal(2, ((IReadOnlyList<MessageContentChunk>)message.Content).Count);
        Assert.Equal(contentChunk1, ((IReadOnlyList<MessageContentChunk>)message.Content)[0]);
        Assert.Equal(contentChunk2, ((IReadOnlyList<MessageContentChunk>)message.Content)[1]);
    }

    [Fact]
    public void CreateUserMessage_WithTextContent_SetsRoleAndContent()
    {
        // arrange
        const string content = "Hello";

        // act
        var message = Message.CreateUserMessageWithTextContent(content);

        // assert
        Assert.Equal("user", message.Role);
        Assert.IsType<TextContentChunk>(message.Content, exactMatch: false);
        Assert.Equal(content, ((TextContentChunk)message.Content).Text);      
    }

    [Fact]
    public void CreateUserMessage_WithImageUrlContent_SetsRoleAndContent()
    {
        // arrange
        const string imageUrl = "https://pl.wikipedia.org/static/images/icons/wikipedia.png";

        // act
        var message = Message.CreateUserMessageWithImageUrlContent(imageUrl);
    
        // assert
        Assert.Equal("user", message.Role);
        Assert.IsType<ImageUrlContentChunk>(message.Content, exactMatch: false);
        Assert.Equal(imageUrl, ((ImageUrlContentChunk)message.Content).ImageUrl.Url);
    }

    [Fact]
    public void CreateUserMessage_WithVideoUrlContent_SetsRoleAndContent()
    {
        // arrange
        const string videoUrl = "https://www.w3schools.com/tags/mov_bbb.mp4";

        // act
        var message = Message.CreateUserMessageWithVideoUrlContent(videoUrl);
    
        // assert
        Assert.Equal("user", message.Role);
        Assert.IsType<VideoUrlContentChunk>(message.Content, exactMatch: false);
        Assert.Equal(videoUrl, ((VideoUrlContentChunk)message.Content).VideoUrl.Url);
    }

    [Fact]
    public void CreateUserMessage_WithFileUrlContent_SetsRoleAndContent()
    {
        // arrange
        const string fileUrl = "https://morth.nic.in/sites/default/files/dd12-13_0.pdf";
        const string fileName = "Dummy file.pdf";

        // act
        var message = Message.CreateUserMessageWithFileUrlContent(fileUrl, fileName);

        // assert
        Assert.Equal("user", message.Role);
        Assert.IsType<FileUrlContentChunk>(message.Content, exactMatch: false);
        Assert.Equal(fileUrl, ((FileUrlContentChunk)message.Content).FileUrl.Url);
        Assert.Equal(fileName, ((FileUrlContentChunk)message.Content).FileName);
    }

    [Fact]
    public void CreateUserMessage_WithPdfUrlContent_SetsRoleAndContent()
    {
        // arrange
        const string pdfUrl = "https://morth.nic.in/sites/default/files/dd12-13_0.pdf";

        // act
        var message = Message.CreateUserMessageWithPdfUrlContent(pdfUrl);
    
        // assert
        Assert.Equal("user", message.Role);
        Assert.IsType<PdfUrlContentChunk>(message.Content, exactMatch: false);
        Assert.Equal(pdfUrl, ((PdfUrlContentChunk)message.Content).PdfUrl.Url);
    }

    [Fact]
    public void CreateAssistantMessage_WithContent_SetsRoleAndContent()
    {
        // arrange
        const string content = "Hi there!";

        // act
        var message = Message.CreateAssistantMessage(content);

        // assert
        Assert.Equal("assistant", message.Role);
        Assert.Equal(content, message.Content);
    }

    [Fact]
    public void CreateAssistantMessage_WithTextContent_SetsRoleAndContent()
    {
        // arrange
        const string content = "Hi there!";

        // act
        var message = Message.CreateAssistantMessageWithTextContent(content);

        // assert
        Assert.Equal("assistant", message.Role);
        Assert.IsType<TextContentChunk>(message.Content, exactMatch: false);
        Assert.Equal(content, ((TextContentChunk)message.Content).Text);
    }

    [Fact]
    public void CreateAssistantMessage_WithImageUrlContent_SetsRoleAndContent()
    {
        // arrange
        const string imageUrl = "https://pl.wikipedia.org/static/images/icons/wikipedia.png";

        // act
        var message = Message.CreateAssistantMessageWithImageUrlContent(imageUrl);

        // assert
        Assert.Equal("assistant", message.Role);
        Assert.IsType<ImageUrlContentChunk>(message.Content, exactMatch: false);
        Assert.Equal(imageUrl, ((ImageUrlContentChunk)message.Content).ImageUrl.Url);
    }

    [Fact]
    public void CreateAssistantMessage_WithVideoUrlContent_SetsRoleAndContent()
    {
        // arrange
        const string videoUrl = "https://www.w3schools.com/tags/mov_bbb.mp4";

        // act
        var message = Message.CreateAssistantMessageWithVideoUrlContent(videoUrl);

        // assert
        Assert.Equal("assistant", message.Role);
        Assert.IsType<VideoUrlContentChunk>(message.Content, exactMatch: false);
        Assert.Equal(videoUrl, ((VideoUrlContentChunk)message.Content).VideoUrl.Url);
    }

    [Fact]
    public void CreateAssistantMessage_WithFileUrlContent_SetsRoleAndContent()
    {
        // arrange
        const string fileUrl = "https://morth.nic.in/sites/default/files/dd12-13_0.pdf";
        const string fileName = "Dummy file.pdf";

        // act
        var message = Message.CreateAssistantMessageWithFileUrlContent(fileUrl, fileName);

        // assert
        Assert.Equal("assistant", message.Role);
        Assert.IsType<FileUrlContentChunk>(message.Content, exactMatch: false);
        Assert.Equal(fileUrl, ((FileUrlContentChunk)message.Content).FileUrl.Url);
        Assert.Equal(fileName, ((FileUrlContentChunk)message.Content).FileName);
    }

    [Fact]
    public void CreateAssistantMessage_WithPdfUrlContent_SetsRoleAndContent()
    {
        // arrange
        const string pdfUrl = "https://morth.nic.in/sites/default/files/dd12-13_0.pdf";

        // act
        var message = Message.CreateAssistantMessageWithPdfUrlContent(pdfUrl);

        // assert
        Assert.Equal("assistant", message.Role);
        Assert.IsType<PdfUrlContentChunk>(message.Content, exactMatch: false);
        Assert.Equal(pdfUrl, ((PdfUrlContentChunk)message.Content).PdfUrl.Url);
    }

    [Fact]
    public void CreateSystemMessage_WithContent_SetsRoleAndContent()
    {
        // arrange
        const string content = "You are a helpful assistant.";

        // act
        var message = Message.CreateSystemMessage(content);

        // assert
        Assert.Equal("system", message.Role);
        Assert.Equal(content, message.Content);
    }

    [Fact]
    public void CreateSystemMessage_WithTextContent_SetsRoleAndContent()
    {
        // arrange
        const string content = "You are a helpful assistant.";

        // act
        var message = Message.CreateSystemMessageWithTextContent(content);

        // assert
        Assert.Equal("system", message.Role);
        Assert.IsType<TextContentChunk>(message.Content, exactMatch: false);
        Assert.Equal(content, ((TextContentChunk)message.Content).Text);
    }

    [Fact]
    public void CreateSystemMessage_WithImageUrlContent_SetsRoleAndContent()
    {
        // arrange
        const string imageUrl = "https://pl.wikipedia.org/static/images/icons/wikipedia.png";

        // act
        var message = Message.CreateSystemMessageWithImageUrlContent(imageUrl);

        // assert
        Assert.Equal("system", message.Role);
        Assert.IsType<ImageUrlContentChunk>(message.Content, exactMatch: false);
        Assert.Equal(imageUrl, ((ImageUrlContentChunk)message.Content).ImageUrl.Url);
    }

    [Fact]
    public void CreateSystemMessage_WithVideoUrlContent_SetsRoleAndContent()
    {
        // arrange
        const string videoUrl = "https://www.w3schools.com/tags/mov_bbb.mp4";

        // act
        var message = Message.CreateSystemMessageWithVideoUrlContent(videoUrl);

        // assert
        Assert.Equal("system", message.Role);
        Assert.IsType<VideoUrlContentChunk>(message.Content, exactMatch: false);
        Assert.Equal(videoUrl, ((VideoUrlContentChunk)message.Content).VideoUrl.Url);
    }

    [Fact]
    public void CreateSystemMessage_WithFileUrlContent_SetsRoleAndContent()
    {
        // arrange
        const string fileUrl = "https://morth.nic.in/sites/default/files/dd12-13_0.pdf";
        const string fileName = "Dummy file.pdf";

        // act
        var message = Message.CreateSystemMessageWithFileUrlContent(fileUrl, fileName);

        // assert
        Assert.Equal("system", message.Role);
        Assert.IsType<FileUrlContentChunk>(message.Content, exactMatch: false);
        Assert.Equal(fileUrl, ((FileUrlContentChunk)message.Content).FileUrl.Url);
        Assert.Equal(fileName, ((FileUrlContentChunk)message.Content).FileName);
    }

    [Fact]
    public void CreateSystemMessage_WithPdfUrlContent_SetsRoleAndContent()
    {
        // arrange
        const string pdfUrl = "https://morth.nic.in/sites/default/files/dd12-13_0.pdf";

        // act
        var message = Message.CreateSystemMessageWithPdfUrlContent(pdfUrl);

        // assert
        Assert.Equal("system", message.Role);
        Assert.IsType<PdfUrlContentChunk>(message.Content, exactMatch: false);
        Assert.Equal(pdfUrl, ((PdfUrlContentChunk)message.Content).PdfUrl.Url);
    }

    [Fact]
    public void CreateToolMessage_WithContent_SetsRoleAndContent()
    {
        // arrange
        const string content = "{\"result\": \"ok\"}";

        // act
        var message = Message.CreateToolMessage(content);

        // assert
        Assert.Equal("tool", message.Role);
        Assert.Equal(content, message.Content);
    }
}