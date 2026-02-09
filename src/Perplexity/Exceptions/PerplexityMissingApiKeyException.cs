namespace Perplexity.Exceptions;

public class PerplexityMissingApiKeyException() 
    : InvalidOperationException("Environment variable 'PERPLEXITY_APIKEY' is required but was not found.");