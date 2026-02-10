using Perplexity.Exceptions;

namespace Perplexity;

public static class PerplexityMissingApiKey
{
    public const string Name = "PERPLEXITY_API_KEY";
    
    public static string Value => Environment.GetEnvironmentVariable(Name) ?? throw new PerplexityMissingApiKeyException();
}