using Perplexity;
using Perplexity.Chat.Dtos;

Console.WriteLine("Perplexity Console Example");
Console.WriteLine();
var perplexityClient = new PerplexityClient();
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
if (response.IsSuccess)
{
    Console.WriteLine("SUCCESS");
    Console.WriteLine("MESSAGES");
    foreach (var choice in response.Data.Choices)
    {
        Console.WriteLine(choice.Message.Content);
    }
    Console.WriteLine();
    Console.WriteLine("CITATIONS");
    foreach (var citation in response.Data.Citations)
    {
        Console.WriteLine(citation);
    }
}
else
{
    Console.WriteLine("FAILED");
}