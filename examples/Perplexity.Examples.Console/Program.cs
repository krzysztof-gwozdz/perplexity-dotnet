using Perplexity;
using Perplexity.Chat.Dtos;
using Perplexity.Exceptions;

Console.WriteLine("Perplexity Console Example");
Console.WriteLine();
var apikey = Environment.GetEnvironmentVariable("PERPLEXITY_APIKEY") ?? throw new PerplexityMissingApiKeyException();
var perplexityClient = new PerplexityClient(apikey);
var chatClient = perplexityClient.ChatClient;
var request = new CreateChatCompletionRequest
{
    Model = "sonar",
    Messages =
    [
        Message.CreateUserMessage("Hello, how are you?")
    ]
};
var response = await chatClient.CreateChatCompletion(request);
Console.WriteLine("MESSAGES");
foreach (var choice in response.Choices)
{
    Console.WriteLine(choice.Message.Content);
}
Console.WriteLine();
Console.WriteLine("CITATIONS");
foreach (var citation in response.Citations)
{
    Console.WriteLine(citation);
}