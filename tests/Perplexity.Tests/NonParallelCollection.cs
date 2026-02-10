namespace Perplexity.Tests;

/// <summary>
/// Collection used to run tests sequentially (e.g. tests that modify process-wide state like environment variables).
/// </summary>
[CollectionDefinition("NonParallel", DisableParallelization = true)]
public class NonParallelCollection;
