using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace LightSpeed.Api.Client.Configuration;

/// <summary>
/// Used for authenticating with LightSpeed API with a personal token.
/// </summary>
public class TokenApiBase : ILightSpeedApiConfiguration
{
    public required string ApiToken { get; init; }
    public required string StoreDomain { get; init; }
    public TokenApiBase(string storeDomain, string apiToken)
    {
        StoreDomain = storeDomain;
        ApiToken = apiToken;
    }
}