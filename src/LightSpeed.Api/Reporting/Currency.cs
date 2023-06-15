using Newtonsoft.Json;

namespace LightSpeed.Api.Reporting;

public class Currency
{

    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}