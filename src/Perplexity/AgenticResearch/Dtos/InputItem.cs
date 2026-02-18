#nullable disable
using System.Text.Json.Serialization;

namespace Perplexity.AgenticResearch.Dtos;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(InputMessage), "message")]
[JsonDerivedType(typeof(FunctionCallInput), "function_call")]
[JsonDerivedType(typeof(FunctionCallOutputInput), "function_call_output")]
public abstract record InputItem;

public record InputMessage : InputItem
{
    [JsonPropertyName("role")]
    public string Role { get; init; }

    [JsonPropertyName("content")]
    public string Content { get; init; }
}

public record FunctionCallInput : InputItem
{
    [JsonPropertyName("call_id")]
    public string CallId { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("arguments")]
    public string Arguments { get; init; }

    [JsonPropertyName("thought_signature")]
    public string ThoughtSignature { get; init; }
}

public record FunctionCallOutputInput : InputItem
{
    [JsonPropertyName("call_id")]
    public string CallId { get; init; }

    [JsonPropertyName("output")]
    public string Output { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("thought_signature")]
    public string ThoughtSignature { get; init; }
}
