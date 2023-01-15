using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightSpeed.Api.Shared.Interfaces;
using Newtonsoft.Json;

namespace LightSpeed.Api.TestApp.Models;
public class LightSpeedStore : ILightSpeedStore, IApiToken
{
    [JsonProperty("store_domain")]
    public required string StoreDomain { get; init; }
    [JsonProperty("api_token")]
    public required string ApiToken { get; init; }
}
