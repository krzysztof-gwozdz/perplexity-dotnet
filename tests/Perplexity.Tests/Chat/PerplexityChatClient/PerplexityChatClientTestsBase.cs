using Perplexity.Chat;

namespace Perplexity.Tests.Chat.PerplexityChatClient;

public abstract class PerplexityChatClientTestsBase
{
    protected IPerplexityChatClient ChatClient { get; private set; }

    protected PerplexityChatClientTestsBase()
    {
        var perplexityClient = new PerplexityClient();
        ChatClient = perplexityClient.ChatClient;
    }
}