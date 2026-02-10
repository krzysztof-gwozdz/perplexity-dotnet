using Perplexity.Exceptions;

namespace Perplexity.Tests;

[Collection("NonParallel")]
public class PerplexityClientTests
{
    [Fact]
    public void Constructor_WithoutParameters_CreatesClientWithSubClients()
    {
        // act
        var perplexityClient = new PerplexityClient();

        // assert
        Assert.NotNull(perplexityClient.ChatClient);
        Assert.NotNull(perplexityClient.SearchClient);
        Assert.NotNull(perplexityClient.AuthenticationClient);
        Assert.NotNull(perplexityClient.AgenticResearchClient);
    }

    [Fact]
    public void Constructor_WithApiKey_CreatesClientWithSubClients()
    {
        // act
        var perplexityClient = new PerplexityClient(PerplexityMissingApiKey.Value);

        // assert
        Assert.NotNull(perplexityClient.ChatClient);
        Assert.NotNull(perplexityClient.SearchClient);
        Assert.NotNull(perplexityClient.AuthenticationClient);
        Assert.NotNull(perplexityClient.AgenticResearchClient);
    }

    [Fact]
    public void Constructor_WithApiKeyAndHttpClient_CreatesClientWithSubClients()
    {
        // act
        var perplexityClient = new PerplexityClient(new HttpClient(), PerplexityMissingApiKey.Value);

        // assert
        Assert.NotNull(perplexityClient.ChatClient);
        Assert.NotNull(perplexityClient.SearchClient);
        Assert.NotNull(perplexityClient.AuthenticationClient);
        Assert.NotNull(perplexityClient.AgenticResearchClient);
    }
    
    [Fact]
    public void Constructor_WithoutParametersAndApiKeyNotSet_ThrowsException()
    {
        // arrange
        var previousValue = Environment.GetEnvironmentVariable(PerplexityMissingApiKey.Name);
        try
        {
            Environment.SetEnvironmentVariable(PerplexityMissingApiKey.Name, null);

            // act
            var constructor = () => new PerplexityClient();
            
            // assert
            var exception = Assert.Throws<PerplexityMissingApiKeyException>(constructor);
            Assert.Equal($"Environment variable '{PerplexityMissingApiKey.Name}' is required but was not found.", exception.Message);
        }
        finally
        {
            Environment.SetEnvironmentVariable(PerplexityMissingApiKey.Name, previousValue ?? null);
        }
    }
}