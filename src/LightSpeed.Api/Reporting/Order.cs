using Newtonsoft.Json;

namespace LightSpeed.Api.Reporting;

public class Order
{

    [JsonProperty("dimension")]
    public string Dimension { get; set; }

    [JsonProperty("direction")]
    public string Direction { get; set; }

    [JsonProperty("metric")]
    public string Metric { get; set; }

    [JsonProperty("alphabetical")]
    public bool Alphabetical { get; set; }
}