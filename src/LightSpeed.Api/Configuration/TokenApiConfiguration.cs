using System.Diagnostics.CodeAnalysis;
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
}