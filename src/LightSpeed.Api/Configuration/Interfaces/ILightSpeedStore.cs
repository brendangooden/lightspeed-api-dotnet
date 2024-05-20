namespace LightSpeed.Api.Configuration.Interfaces;

public interface ILightSpeedStore
{
    /// <summary>
    /// The domain portion of the store URL e.g. "outpost"  from https://outpost.vendhq.com
    /// </summary>
    string StoreDomain { get; }
}