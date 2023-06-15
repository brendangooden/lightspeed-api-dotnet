using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LightSpeed.Api.Client.Extensions;
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
