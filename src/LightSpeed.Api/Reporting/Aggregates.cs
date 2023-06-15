using System.Collections.Generic;
using Newtonsoft.Json;

namespace LightSpeed.Api.Reporting;

public class Aggregates
{

    [JsonProperty("cell", NullValueHandling = NullValueHandling.Ignore)]
    public IList<string> Cell { get; set; }

    [JsonProperty("row", NullValueHandling = NullValueHandling.Ignore)]
    public IList<string> Row { get; set; }
    [JsonProperty("column", NullValueHandling = NullValueHandling.Ignore)]
    public IList<string> Column { get; set; }
    [JsonProperty("table", NullValueHandling = NullValueHandling.Ignore)]
    public IList<string> Table { get; set; }
}