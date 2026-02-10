namespace Perplexity.Exceptions;

public class PerplexityMissingApiKeyException() 
    : InvalidOperationException($"Environment variable '{PerplexityMissingApiKey.Name}' is required but was not found.");