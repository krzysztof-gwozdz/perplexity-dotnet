using Perplexity;
using Perplexity.Chat.Dtos;
using Perplexity.Exceptions;

Console.WriteLine("Perplexity Console Example");
var apikey = Environment.GetEnvironmentVariable("PERPLEXITY_APIKEY") ?? throw new PerplexityMissingApiKeyException();
var perplexityClient = new PerplexityClient(apikey);
var chatClient = perplexityClient.ChatClient;
var request = new CreateChatCompletionRequest
{
    Model = "sonar",
    Messages =
    [
        new ChatCompletionMessage { Role = "user", Content = "Hello, how are you?" }
    ]
};
var response = await chatClient.CreateChatCompletion(request);
Console.WriteLine(response.Choices[0].Message.Content);