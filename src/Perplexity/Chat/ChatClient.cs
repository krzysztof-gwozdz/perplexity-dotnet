using Perplexity.Chat.Dtos;
using Perplexity.Common;

namespace Perplexity.Chat;

public class PerplexityChatClient(HttpClient httpClient, string apiKey) : BaseClient(httpClient, apiKey)
{
    public async Task<Result<CreateChatCompletionResponse>> CreateChatCompletion(CreateChatCompletionRequest request, CancellationToken cancellationToken = default) => 
        await Post<CreateChatCompletionRequest, CreateChatCompletionResponse>("/chat/completions", request, cancellationToken);

    public async Task<Result<CreateAsyncChatCompletionResponse>> CreateAsyncChatCompletion(CreateAsyncChatCompletionRequest request, CancellationToken cancellationToken = default) => 
        await Post<RequestDto<CreateAsyncChatCompletionRequest>, CreateAsyncChatCompletionResponse>("/async/chat/completions", new RequestDto<CreateAsyncChatCompletionRequest> { Request = request }, cancellationToken);

    public async Task<Result<ListAsyncChatCompletionsResponse>> ListAsyncChatCompletions(CancellationToken cancellationToken = default) => 
        await Get<ListAsyncChatCompletionsResponse>("/async/chat/completions", cancellationToken);

    public async Task<Result<GetAsyncChatCompletionResponse>> GetAsyncChatCompletion(GetAsyncChatCompletionParams @params, CancellationToken cancellationToken = default) => 
        await Get<GetAsyncChatCompletionResponse>($"/async/chat/completions/{@params.ApiRequest}?local_mode={@params.LocalMode}", cancellationToken);
}
