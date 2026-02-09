using Perplexity.Chat.Dtos;

namespace Perplexity.Chat;

public class PerplexityChatClient(HttpClient httpClient) : BaseClient(httpClient)
{
    public async Task<CreateChatCompletionResponse> CreateChatCompletion(CreateChatCompletionRequest request, CancellationToken cancellationToken = default)
    {
        var response = await Post<CreateChatCompletionRequest, CreateChatCompletionResponse>("/chat/completions", request, cancellationToken);
        return response;
    }
}