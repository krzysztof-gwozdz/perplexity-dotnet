using Perplexity.Chat.Dtos;
using Perplexity.Common;

namespace Perplexity.Chat;

public class PerplexityChatClient(HttpClient httpClient) : BaseClient(httpClient)
{
    public async Task<CreateChatCompletionResponse> CreateChatCompletion(CreateChatCompletionRequest request, CancellationToken cancellationToken = default)
    {
        var response = await Post<CreateChatCompletionRequest, CreateChatCompletionResponse>("/chat/completions", request, cancellationToken);
        return response;
    }

    public async Task<CreateAsyncChatCompletionResponse> CreateAsyncChatCompletion(CreateAsyncChatCompletionRequest request, CancellationToken cancellationToken = default)
    {
        var response = await Post<RequestDto<CreateAsyncChatCompletionRequest>, CreateAsyncChatCompletionResponse>("/async/chat/completions", new RequestDto<CreateAsyncChatCompletionRequest> { Request = request }, cancellationToken);
        return response;
    }
    
    public async Task<ListAsyncChatCompletionsResponse> ListAsyncChatCompletions(CancellationToken cancellationToken = default)
    {
        var response = await Get<ListAsyncChatCompletionsResponse>("/async/chat/completions", cancellationToken);
        return response;
    }
    
    public async Task<GetAsyncChatCompletionResponse> GetAsyncChatCompletion(GetAsyncChatCompletionParams @params, CancellationToken cancellationToken = default)
    {
        var url = $"/async/chat/completions/{@params.ApiRequest}?local_mode={@params.LocalMode}";
        var response = await Get<GetAsyncChatCompletionResponse>(url, cancellationToken);
        return response;
    }
}