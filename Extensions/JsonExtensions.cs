using Newtonsoft.Json;

namespace LightSpeed.Api.Client.Extensions
{
    public static class ExtensionMethods
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
