using System;
using Newtonsoft.Json;

namespace LightSpeed.Api.Reporting;

public class Period
{

    [JsonProperty("start")]
    public DateTime Start { get; set; }

    [JsonProperty("end")]
    public DateTime End { get; set; }

    [JsonProperty("denormalise_time")]
    public bool DenormaliseTime { get; set; }
}