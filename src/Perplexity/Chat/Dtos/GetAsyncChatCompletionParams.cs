#nullable disable

namespace Perplexity.Chat.Dtos;

public class GetAsyncChatCompletionParams
{
    public string ApiRequest { get; init; }

    public bool LocalMode { get; init; }
}