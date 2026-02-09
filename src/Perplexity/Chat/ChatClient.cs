using Perplexity.Chat.Dtos;

namespace Perplexity.Chat;

public class PerplexityChatClient(HttpClient httpClient) : BaseClient(httpClient)
{
    private const string Url = "/chat/completions";

    public async Task<CreateChatCompletionResponse> CreateChatCompletion(CreateChatCompletionRequest request, CancellationToken cancellationToken = default)
    {
        var response = await Post<CreateChatCompletionRequest, CreateChatCompletionResponse>(Url, request, cancellationToken);
        return response;
    }
}