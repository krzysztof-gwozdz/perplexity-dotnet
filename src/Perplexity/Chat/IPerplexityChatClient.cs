using Perplexity.Chat.Dtos;
using Perplexity.Common;

namespace Perplexity.Chat;

public interface IPerplexityChatClient
{
    Task<Result<CreateChatCompletionResponse>> CreateChatCompletion(CreateChatCompletionRequest request, CancellationToken cancellationToken = default);
    Task<Result<CreateAsyncChatCompletionResponse>> CreateAsyncChatCompletion(CreateAsyncChatCompletionRequest request, CancellationToken cancellationToken = default);
    Task<Result<ListAsyncChatCompletionsResponse>> ListAsyncChatCompletions(CancellationToken cancellationToken = default);
    Task<Result<GetAsyncChatCompletionResponse>> GetAsyncChatCompletion(GetAsyncChatCompletionParams @params, CancellationToken cancellationToken = default);
}