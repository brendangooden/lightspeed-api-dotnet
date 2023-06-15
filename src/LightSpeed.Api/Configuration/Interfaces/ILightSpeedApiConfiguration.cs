using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace LightSpeed.Api.Client.Configuration.Interfaces;

/// <summary>
/// A base interface used by the LightSpeedApiClient on how to authenticate requests, and what the base URL is.
/// </summary>
public interface ILightSpeedApiConfiguration : ILightSpeedStore, IApiToken
{
    /// <summary>
    /// The base URL of the API including the version.
    /// Default is <c>https://{<see cref="ILightSpeedStore.StoreDomain"/>}.vendhq.com/api/2.0</c>
    /// </summary>
    string BaseUrl => $"https://{StoreDomain}.vendhq.com/api/2.0";
    Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
    {
        var msg = new HttpRequestMessage();

        msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ApiToken);

        // no external calls required, so just return a completed task with the HttpRequestMessage object.
        return Task.FromResult(msg);
    }
}