# Perplexity .NET API library

An unofficial .NET client library for the Perplexity API, providing a simple and idiomatic way to integrate Perplexity’s AI capabilities into .NET applications.

### Disclaimer

This project is **not affiliated with, endorsed by, or related to Perplexity AI, Inc.** or any of its subsidiaries. "Perplexity" and related names and marks are trademarks of their respective owners. This is an independent, unofficial library created for developer convenience. Use of the Perplexity name and API is for descriptive purposes only and does not imply any endorsement or permission from the trademark holder.

## Table of Contents

- [Disclaimer](#disclaimer)
- [Getting started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Install the NuGet package](#install-the-nuget-package)
- [Using the client library](#using-the-client-library)
    - [Namespace organization](#namespace-organization)
    - [Using the async API](#using-the-async-api)
- [Chat completions endpoints](#chat-completions-endpoints)
- [Search endpoints](#search-endpoints)
- [Embeddings endpoints](#embeddings-endpoints)
- [Agentic research endpoints](#agentic-research-endpoints)
- [Authentication endpoints](#authentication-endpoints)
- [Error handling](#error-handling)

## Getting started

### Prerequisites

To call the Perplexity REST API, you will need an API key. To obtain one,
first [create a new Perplexity account](https://www.perplexity.ai/) or [log in](https://www.perplexity.ai/). Next,
navigate to the [API settings page](https://www.perplexity.ai/settings/api) to generate your API key. Make sure to save
your API key somewhere safe and do not share it with anyone.

### Install the NuGet package

The library is distributed as a NuGet package **Perplexity.Unofficial**.

**Using the .NET CLI:**

```bash
dotnet add package Perplexity.Unofficial
```

**Using the Package Manager Console (Visual Studio):**

```powershell
Install-Package Perplexity.Unofficial
```

## Using the client library

The following snippet illustrates the basic use of the chat completions API:

```C# Snippet:ReadMe_ChatCompletion_Basic
using Perplexity;
using Perplexity.Chat.Dtos;

var client = new PerplexityClient(Environment.GetEnvironmentVariable("PERPLEXITY_API_KEY"));
var chatClient = client.ChatClient;

var request = new CreateChatCompletionRequest
{
    Model = ChatCompletionModels.Sonar,
    Messages = [Message.CreateUserMessage("Hello, how are you?")]
};

var response = await chatClient.CreateChatCompletion(request);
if (response.IsSuccess)
{
    foreach (var choice in response.Data.Choices)
    {
        Console.WriteLine($"[ASSISTANT]: {choice.Message.Content}");
    }
}
```

While you can pass your API key directly as a string, it is highly recommended that you keep it in a secure location and
instead access it via an environment variable or configuration file as shown above to avoid storing it in source
control.

### Namespace organization

The library is organized into namespaces by feature areas in the Perplexity REST API. Each namespace contains a
corresponding client interface and implementation.

| Namespace                    | Client interface                   | Client class                      |
|------------------------------|------------------------------------|-----------------------------------|
| `Perplexity.Chat`            | `IPerplexityChatClient`            | `PerplexityChatClient`            |
| `Perplexity.Search`          | `IPerplexitySearchClient`          | `PerplexitySearchClient`          |
| `Perplexity.Embeddings`      | `IPerplexityEmbeddingsClient`      | `PerplexityEmbeddingsClient`      |
| `Perplexity.AgenticResearch` | `IPerplexityAgenticResearchClient` | `PerplexityAgenticResearchClient` |
| `Perplexity.Authentication`  | `IPerplexityAuthenticationClient`  | `PerplexityAuthenticationClient`  |

The main `PerplexityClient` class provides access to all feature area clients through properties:

```C# Snippet:ReadMe_PerplexityClient_Usage
using Perplexity;

var client = new PerplexityClient(Environment.GetEnvironmentVariable("PERPLEXITY_API_KEY"));

// Access individual clients
var chatClient = client.ChatClient;
var searchClient = client.SearchClient;
var embeddingsClient = client.EmbeddingsClient;
var agenticResearchClient = client.AgenticResearchClient;
var authenticationClient = client.AuthenticationClient;
```

### Using the async API

All client methods are asynchronous and return `Task<Result<T>>` or `Task<Result>`. The examples throughout this
document use `await` to handle asynchronous operations:

```C# Snippet:ReadMe_ChatCompletion_Async
var response = await chatClient.CreateChatCompletion(request);
```

## Chat completions endpoints

Chat completions allow you to have conversations with Perplexity's models. The API supports various models; use
`ChatCompletionModels` in `Perplexity.Chat.Dtos` for the documented Sonar model ids (for example `Sonar`, `SonarPro`).

### Basic chat completion

```C# Snippet:ReadMe_ChatCompletion_BasicExample
using Perplexity;
using Perplexity.Chat.Dtos;

var client = new PerplexityClient(Environment.GetEnvironmentVariable("PERPLEXITY_API_KEY"));
var chatClient = client.ChatClient;

var request = new CreateChatCompletionRequest
{
    Model = ChatCompletionModels.Sonar,
    Messages =
    [
        Message.CreateUserMessage("What is the capital of France?")
    ]
};

var response = await chatClient.CreateChatCompletion(request);
if (response.IsSuccess)
{
    foreach (var choice in response.Data.Choices)
    {
        Console.WriteLine($"[ASSISTANT]: {choice.Message.Content}");
    }
    
    // Access citations if available
    if (response.Data.Citations != null)
    {
        Console.WriteLine("\nCitations:");
        foreach (var citation in response.Data.Citations)
        {
            Console.WriteLine($"  - {citation}");
        }
    }
}
```

### Multi-turn conversations

```C# Snippet:ReadMe_ChatCompletion_MultiTurn
using Perplexity;
using Perplexity.Chat.Dtos;

var client = new PerplexityClient(Environment.GetEnvironmentVariable("PERPLEXITY_API_KEY"));
var chatClient = client.ChatClient;

var messages = new List<Message>
{
    Message.CreateSystemMessage("You are a helpful assistant."),
    Message.CreateUserMessage("What is the weather like today?"),
};

var request = new CreateChatCompletionRequest
{
    Model = ChatCompletionModels.Sonar,
    Messages = messages
};

var response = await chatClient.CreateChatCompletion(request);
if (response.IsSuccess)
{
    // Add assistant response to conversation history
    var assistantMessage = response.Data.Choices[0].Message;
    messages.Add(assistantMessage);
    
    // Continue conversation
    messages.Add(Message.CreateUserMessage("What about tomorrow?"));
    
    var followUpRequest = new CreateChatCompletionRequest
    {
        Model = ChatCompletionModels.Sonar,
        Messages = messages
    };
    
    var followUpResponse = await chatClient.CreateChatCompletion(followUpRequest);
    // Process follow-up response...
}
```

### Async chat completions

For long-running requests, you can use async chat completions:

```C# Snippet:ReadMe_ChatCompletion_AsyncExample

using Perplexity;
using Perplexity.Chat.Dtos;

var client = new PerplexityClient(Environment.GetEnvironmentVariable("PERPLEXITY_API_KEY"));
var chatClient = client.ChatClient;

var asyncRequest = new CreateAsyncChatCompletionRequest
{
    Model = ChatCompletionModels.Sonar,
    Messages = [Message.CreateUserMessage("Tell me about quantum computing")]
};

var asyncResponse = await chatClient.CreateAsyncChatCompletion(asyncRequest);
if (asyncResponse.IsSuccess)
{
    var requestId = asyncResponse.Data.Id;
    
    // Poll for completion
    var getParams = new GetAsyncChatCompletionParams
    {
        ApiRequest = requestId,
        LocalMode = false
    };
    
    while (true)
    {
        var statusResponse = await chatClient.GetAsyncChatCompletion(getParams);
        if (statusResponse.IsSuccess && statusResponse.Data.Status == "completed")
        {
            // Process completed response
            break;
        }
        await Task.Delay(1000); // Wait 1 second before polling again
    }
}
```

## Search endpoints

The Search API allows you to perform web searches and retrieve search results:

```C# Snippet:ReadMe_Search_Basic
using Perplexity;
using Perplexity.Search.Dtos;

var client = new PerplexityClient(Environment.GetEnvironmentVariable("PERPLEXITY_API_KEY"));
var searchClient = client.SearchClient;

var searchRequest = new SearchRequest
{
    Query = "latest AI developments 2024",
    MaxResults = 5
};

var response = await searchClient.Search(searchRequest);
if (response.IsSuccess)
{
    foreach (var result in response.Data.Results)
    {
        Console.WriteLine($"{result.Title}: {result.Url}");
        Console.WriteLine(result.Snippet);
        Console.WriteLine();
    }
}
```

### Advanced search with filters

```C# Snippet:ReadMe_Search_Advanced
using Perplexity;
using Perplexity.Search.Dtos;

var client = new PerplexityClient(Environment.GetEnvironmentVariable("PERPLEXITY_API_KEY"));
var searchClient = client.SearchClient;

var advancedSearchRequest = new SearchRequest
{
    Query = "machine learning",
    MaxResults = 10,
    SearchDomainFilter = ["arxiv.org", "github.com"],
    SearchRecencyFilter = "month",
    Country = "US"
};

var response = await searchClient.Search(advancedSearchRequest);
// Process results...
```

## Embeddings endpoints

The [Embeddings API](https://docs.perplexity.ai/api-reference/embeddings-post) returns vectors as base64-encoded int8 values by default. The request `input` field is modeled as a list of strings; pass a single text as a one-element array.

```C# Snippet:ReadMe_Embeddings_Basic
using Perplexity;
using Perplexity.Embeddings;
using Perplexity.Embeddings.Dtos;

var client = new PerplexityClient(Environment.GetEnvironmentVariable("PERPLEXITY_API_KEY"));
var embeddingsClient = client.EmbeddingsClient;

var request = new EmbeddingsRequest
{
    Input = ["Hello, world"],
    Model = EmbeddingModels.PplxEmbedV1_0_6b
};

var response = await embeddingsClient.CreateEmbeddings(request);
if (response.IsSuccess)
{
    foreach (var item in response.Data.Data)
    {
        var vector = EmbeddingBase64.DecodeInt8(item.Embedding);
        // vector is sbyte[] with one signed byte per dimension
    }
}
```

Optional fields include `dimensions` (Matryoshka output size) and `encoding_format` (`EmbeddingsEncodingFormat.Base64Int8` or `Base64Binary`).

### Contextualized embeddings

The [Contextualized Embeddings API](https://docs.perplexity.ai/api-reference/contextualized-embeddings-post) embeds document chunks with shared document-level context. Model `input` as a list of lists: each inner list is one document’s chunks.

```C# Snippet:ReadMe_ContextualizedEmbeddings_Basic
using Perplexity;
using Perplexity.Embeddings;
using Perplexity.Embeddings.Dtos;

var client = new PerplexityClient(Environment.GetEnvironmentVariable("PERPLEXITY_API_KEY"));
var embeddingsClient = client.EmbeddingsClient;

var request = new ContextualizedEmbeddingsRequest
{
    Input =
    [
        ["First chunk of document A", "Second chunk of document A"]
    ],
    Model = ContextualizedEmbeddingModels.PplxEmbedContextV1_0_6b
};

var response = await embeddingsClient.CreateContextualizedEmbeddings(request);
if (response.IsSuccess)
{
    foreach (var doc in response.Data.Data)
    {
        foreach (var chunk in doc.Data)
        {
            var vector = EmbeddingBase64.DecodeInt8(chunk.Embedding);
        }
    }
}
```

## Agentic research endpoints

The Agentic Research API enables more sophisticated research capabilities with tools and multi-step reasoning:

```C# Snippet:ReadMe_AgenticResearch_Basic
using Perplexity;
using Perplexity.AgenticResearch.Dtos;

var client = new PerplexityClient(Environment.GetEnvironmentVariable("PERPLEXITY_API_KEY"));
var agenticResearchClient = client.AgenticResearchClient;

var request = new AgenticResearchRequest
{
    Model = AgenticResearchModels.PerplexitySonar,
    Input = new InputItem[]
    {
        new InputMessage { Role = "user", Content = "What are the latest developments in quantum computing?" }
    },
    Tools = [new WebSearchTool()]
};

var response = await agenticResearchClient.CreateResponse(request);
if (response.IsSuccess)
{
    foreach (var outputItem in response.Data.Output)
    {
        // Process output items (messages, tool calls, etc.)
        Console.WriteLine($"Output type: {outputItem.GetType().Name}");
    }
}
```

### Using tools

```C# Snippet:ReadMe_AgenticResearch_Tools
using Perplexity;
using Perplexity.AgenticResearch.Dtos;

var client = new PerplexityClient(Environment.GetEnvironmentVariable("PERPLEXITY_API_KEY"));
var agenticResearchClient = client.AgenticResearchClient;

var requestWithTools = new AgenticResearchRequest
{
    Model = AgenticResearchModels.PerplexitySonar,
    Input = new InputItem[]
    {
        new InputMessage { Role = "user", Content = "Research the best practices for API design" }
    },
    Tools =
    [
        new WebSearchTool
        {
            Filters = new WebSearchFilters
            {
                DomainFilter = ["stackoverflow.com", "github.com"],
                DateFilter = "year"
            }
        },
        new FetchUrlTool { MaxUrls = 5 }
    ],
    MaxSteps = 10
};

var response = await agenticResearchClient.CreateResponse(requestWithTools);
// Process response...
```

## Authentication endpoints

The Authentication API allows you to manage authentication tokens:

```C# Snippet:ReadMe_Authentication_GenerateToken
using Perplexity;
using Perplexity.Authentication.Dtos;

var client = new PerplexityClient(Environment.GetEnvironmentVariable("PERPLEXITY_API_KEY"));
var authClient = client.AuthenticationClient;

var generateRequest = new GenerateAuthTokenRequest
{
    // Configure token generation parameters
};

var response = await authClient.GenerateAuthToken(generateRequest);
if (response.IsSuccess)
{
    var token = response.Data.Token;
    // Use the generated token...
}
```

### Revoking tokens

```C# Snippet:ReadMe_Authentication_RevokeToken
using Perplexity;
using Perplexity.Authentication.Dtos;

var client = new PerplexityClient(Environment.GetEnvironmentVariable("PERPLEXITY_API_KEY"));
var authClient = client.AuthenticationClient;

var revokeRequest = new RevokeAuthTokenRequest
{
    Token = "token-to-revoke"
};

var response = await authClient.RevokeAuthToken(revokeRequest);
if (response.IsSuccess)
{
    Console.WriteLine("Token revoked successfully");
}
```

## Error handling

The library uses a `Result<T>` pattern for error handling. All API methods return `Task<Result<T>>` or `Task<Result>`,
which allows you to check for success and access error information:

```C# Snippet:ReadMe_ErrorHandling
var response = await chatClient.CreateChatCompletion(request);

if (response.IsSuccess)
{
    // Access the data
    var completion = response.Data;
    // Process successful response...
}
else
{
    // Handle error
    var error = response.Error;
    Console.WriteLine($"Error Code: {error.Code}");
    Console.WriteLine($"Error Type: {error.Type}");
    Console.WriteLine($"Error Message: {error.Message}");
    
    // Access raw request/response if needed
    var rawRequest = response.RawApiRequest;
    var rawResponse = response.RawApiResponse;
}
```

### Exception handling

For connection errors and other exceptions, the library throws `PerplexityClientException`:

```C# Snippet:ReadMe_ExceptionHandling
using Perplexity.Exceptions;

try
{
    var response = await chatClient.CreateChatCompletion(request);
    // Process response...
}
catch (PerplexityClientException ex)
{
    Console.WriteLine($"Connection error: {ex.Message}");
    // Handle connection issues...
}
```

## License

This project is licensed under the MIT License.

[![nuget version](https://img.shields.io/nuget/v/Perplexity.Unofficial)](https://www.nuget.org/packages/Perplexity.Unofficial)
[![nuget downloads](https://img.shields.io/nuget/dt/Perplexity.Unofficial.svg)](https://www.nuget.org/packages/Perplexity.Unofficial)