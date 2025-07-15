using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace LightSpeed.Api.Configuration.Interfaces;

/// <summary>
/// A base interface used by the LightSpeedApiClient on how to authenticate requests, and what the base URL is.
/// </summary>
public interface ILightSpeedApiConfiguration : ILightSpeedStore, IApiToken
{
    /// <summary>
    /// The base URL of the API including the version.
    /// Default is <c>https://{<see cref="ILightSpeedStore.StoreDomain"/>}.retail.lightspeed.app/api/2.0</c>
    /// </summary>
    string BaseUrl => $"https://{StoreDomain}.retail.lightspeed.app/api/2.0";
    /// <summary>
    /// Required method to add the relevant authentication headers/etc. to the request.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken);
}