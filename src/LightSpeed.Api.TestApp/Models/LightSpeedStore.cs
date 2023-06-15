using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightSpeed.Api.Client.Configuration.Interfaces;
using Newtonsoft.Json;

namespace LightSpeed.Api.TestApp.Models;
public class LightSpeedStore : ILightSpeedStore, IApiToken
{
    [JsonProperty("store_domain")]
    public required string StoreDomain { get; set; }
    [JsonProperty("api_token")]
    public required string ApiToken { get; set; }
}
