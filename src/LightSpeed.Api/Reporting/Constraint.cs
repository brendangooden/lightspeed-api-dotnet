using Newtonsoft.Json;

namespace LightSpeed.Api.Reporting;

public class Constraint
{

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}