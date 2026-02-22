using Perplexity.Common;
using Perplexity.Exceptions;

namespace Perplexity.Tests.Common;

public class BaseClientTests
{
    [Fact]
    public async Task Get_ForInvalidUrl_ThrowsPerplexityClientException()
    {
        // arrange
        var perplexityClient = new TestClient(new HttpClient(), string.Empty);

        // act
        var get = async () => await perplexityClient.Get<TestResponse>("invalid:");

        // assert
        await Assert.ThrowsAsync<PerplexityClientException>(get);
    }

    [Fact]
    public async Task PostWithoutResponse_ForInvalidUrl_ThrowsPerplexityClientException()
    {
        // arrange
        var perplexityClient = new TestClient(new HttpClient(), string.Empty);

        // act
        var post = async () => await perplexityClient.Post("invalid:", new TestRequest());

        // assert
        await Assert.ThrowsAsync<PerplexityClientException>(post);
    }

    [Fact]
    public async Task PostWithResponse_ForInvalidUrl_ThrowsPerplexityClientException()
    {
        // arrange
        var perplexityClient = new TestClient(new HttpClient(), string.Empty);

        // act
        var post = async () => await perplexityClient.Post<TestRequest, TestResponse>("invalid:", new TestRequest());

        // assert
        await Assert.ThrowsAsync<PerplexityClientException>(post);
    }
}

public class TestClient(HttpClient httpClient, string apiKey) : BaseClient(httpClient, apiKey)
{
    public new async Task<Result<TResponseDto>> Get<TResponseDto>(string requestUri, CancellationToken cancellationToken = default) =>
        await base.Get<TResponseDto>(requestUri, cancellationToken);

    public new async Task<Result> Post<TRequestDto>(string requestUri, TRequestDto value, CancellationToken cancellationToken = default) =>
        await base.Post(requestUri, value, cancellationToken);

    public new async Task<Result<TResponseDto>> Post<TRequestDto, TResponseDto>(string requestUri, TRequestDto value, CancellationToken cancellationToken = default) =>
        await base.Post<TRequestDto, TResponseDto>(requestUri, value, cancellationToken);
}

public record TestResponse;

public record TestRequest;