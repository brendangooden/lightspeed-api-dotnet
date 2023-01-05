using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace LightSpeed.Api.Client.Configuration
{
    /// <summary>
    /// Used for authenticating with LightSpeed API with a personal token.
    /// </summary>
    public class TokenApiBase : IRestApiConfiguration
    {
        private readonly string _token;
        public string BaseUrl { get; set; }
        public TokenApiBase(string baseUrl, string apiToken)
        {
            BaseUrl = baseUrl;
            _token = apiToken;
        }
        public Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            var msg = new HttpRequestMessage();

            msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // no external calls required, so just return a completed task with the HttpRequestMessage object.
            return Task.FromResult(msg);
        }
    }
}