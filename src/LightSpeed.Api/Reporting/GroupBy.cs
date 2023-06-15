using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LightSpeed.Api.Reporting;

[JsonConverter(typeof(StringEnumConverter))]
public enum GroupBy
{
    day,
    month,
    year
}