using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LightSpeed.Api.Configuration.Interfaces;

namespace LightSpeed.Api.Configuration;

public class ApiClientBase
{
    private readonly ILightSpeedApiConfiguration _config;
    public string BaseUrl => _config.BaseUrl;
    public ApiClientBase(ILightSpeedApiConfiguration config)
    {
        _config = config;
    }
    public async Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
    {
        return await _config.CreateHttpRequestMessageAsync(cancellationToken);
    }
}