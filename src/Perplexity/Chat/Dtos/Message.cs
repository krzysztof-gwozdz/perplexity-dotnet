#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.Chat.Dtos;

public record Message
{
    [JsonPropertyName("role")]
    public string Role { get; init; }

    [JsonPropertyName("content")]
    public string Content { get; init; }
    
    public static Message CreateUserMessage(string content) => 
        new() { Role = "user", Content = content };
    
    public static Message CreateAssistantMessage(string content) => 
        new() { Role = "assistant", Content = content };
    
    public static Message CreateSystemMessage(string content) => 
        new() { Role = "system", Content = content };
    
    public static Message CreateToolMessage(string content) => 
        new() { Role = "tool", Content = content };
}