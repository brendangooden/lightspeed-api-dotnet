using System.Collections.Generic;
using Newtonsoft.Json;

namespace LightSpeed.Api.Reporting;

public class ReportRequest
{

    [JsonProperty("dimensions")]
    public Dimensions Dimensions { get; set; }

    [JsonProperty("aggregates")]
    public Aggregates Aggregates { get; set; }

    [JsonProperty("constraints")]
    public IList<Constraint> Constraints { get; set; }

    [JsonProperty("order")]
    public IList<Order> Order { get; set; }

    [JsonProperty("from")]
    public int From { get; set; }

    [JsonProperty("size")]
    public int Size { get; set; }

    [JsonProperty("periods")]
    public IList<Period> Periods { get; set; }
}