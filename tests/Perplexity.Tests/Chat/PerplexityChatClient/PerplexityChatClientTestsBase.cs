namespace Perplexity.Tests.Chat.PerplexityChatClient;

public abstract class PerplexityChatClientTestsBase
{
    protected Perplexity.Chat.PerplexityChatClient ChatClient { get; private set; }

    protected PerplexityChatClientTestsBase()
    {
        var perplexityClient = new PerplexityClient();
        ChatClient = perplexityClient.ChatClient;
    }
}