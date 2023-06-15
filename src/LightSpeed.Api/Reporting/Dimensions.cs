using System.Collections.Generic;
using Newtonsoft.Json;

namespace LightSpeed.Api.Reporting;

public class Dimensions
{

    [JsonProperty("row")]
    public IList<object> Row { get; set; } = new List<object>();

    [JsonProperty("column")]
    public IList<Column> Column { get; set; }
}