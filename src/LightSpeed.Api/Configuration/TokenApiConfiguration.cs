using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using LightSpeed.Api.Configuration.Interfaces;

namespace LightSpeed.Api.Configuration;

/// <summary>
/// Used for authenticating with LightSpeed API with a personal token.
/// </summary>
public class TokenApiConfiguration : ILightSpeedApiConfiguration
{
    /// <summary>
    /// The personal token used to authenticate with the LightSpeed API.
    /// </summary>
    public required string ApiToken { get; init; }
    /// <summary>
    /// The domain of the store to connect to.
    /// </summary>
    public required string StoreDomain { get; init; }
    /// <summary>
    /// Create a new instance of the <see cref="TokenApiConfiguration"/> class.
    /// </summary>
    /// <param name="storeDomain"></param>
    /// <param name="apiToken"></param>
    [SetsRequiredMembers]
    public TokenApiConfiguration(string storeDomain, string apiToken)
    {
        StoreDomain = storeDomain;
        ApiToken = apiToken;
    }

    /// <summary>
    /// Required method to add the relevant authentication headers/etc. to the request.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
    {
        var msg = new HttpRequestMessage();

        msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ApiToken);

        // no external calls required, so just return a completed task with the HttpRequestMessage object.
        return Task.FromResult(msg);
    }
}