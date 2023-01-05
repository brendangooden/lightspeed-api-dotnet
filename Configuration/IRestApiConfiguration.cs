using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LightSpeed.Api.Client.Configuration
{
    /// <summary>
    /// A base interface used by the LightSpeedApiClient on how to authenticate requests, and what the base URL is.
    /// </summary>
    public interface IRestApiConfiguration
    {
        /// <summary>
        /// The base URL of the API including the version e.g.  <c>https://yourdomain.vendhq.com/api/2.0</c>
        /// /// </summary>
        string BaseUrl { get; }
        Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken);
    }
}