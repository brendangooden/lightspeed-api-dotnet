using System.Diagnostics.CodeAnalysis;

namespace LightSpeed.Api.Configuration;

/// <summary>
/// Used for authenticating with LightSpeed API with a personal token.
/// </summary>
public class TokenApiBase : ILightSpeedApiConfiguration
{
    public required string ApiToken { get; init; }
    public required string StoreDomain { get; init; }
    [SetsRequiredMembers]
    public TokenApiBase(string storeDomain, string apiToken)
    {
        StoreDomain = storeDomain;
        ApiToken = apiToken;
    }
}