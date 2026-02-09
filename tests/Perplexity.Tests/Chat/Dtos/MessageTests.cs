using Perplexity.Chat.Dtos;

namespace Perplexity.Tests.Chat.Dtos;

public class MessageTests
{
    [Fact]
    public void CreateUserMessage_SetsRoleAndContent()
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
    public void CreateAssistantMessage_SetsRoleAndContent()
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
    public void CreateSystemMessage_SetsRoleAndContent()
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
    public void CreateToolMessage_SetsRoleAndContent()
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