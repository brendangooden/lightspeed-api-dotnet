﻿namespace LightSpeed.Api.Configuration.Interfaces;

public interface IApiToken
{
    /// <summary>
    /// Personal Access Token (PAT) used to communicate with the Vend API's
    /// </summary>
    string ApiToken { get; }
}