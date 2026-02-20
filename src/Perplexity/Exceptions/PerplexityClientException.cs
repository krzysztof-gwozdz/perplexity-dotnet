namespace Perplexity.Exceptions;

public class PerplexityClientException(Exception exception)
    : Exception("Internal Perplexity client exception occured.", exception);