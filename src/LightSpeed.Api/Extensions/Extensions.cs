using Newtonsoft.Json;

namespace LightSpeed.Api.Extensions;
internal static class Extensions
{
    internal static string ToJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"
        });
    }
}
