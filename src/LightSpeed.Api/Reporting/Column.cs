using System.Collections.Generic;
using Newtonsoft.Json;

namespace LightSpeed.Api.Reporting;

public class Column
{

    [JsonProperty("key")]
    public GroupBy Key { get; set; }

    [JsonProperty("metadata")]
    public IList<object> Metadata { get; set; } = new List<object>();
}