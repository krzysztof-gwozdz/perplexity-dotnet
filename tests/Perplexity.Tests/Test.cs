namespace Perplexity.Tests;

public class Test
{
    [Fact]
    public void AlwaysTrue()
    {
        var perplexityClient = new PerplexityClient();
        Assert.True(true);
    }
}