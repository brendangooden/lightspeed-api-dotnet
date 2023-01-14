using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LightSpeed.Api.Client;

public partial class LightSpeedApiClient
{
    partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings)
    {
        settings.NullValueHandling = NullValueHandling.Ignore;
        settings.ContractResolver = new SafeContractResolver();
    }

    // See https://github.com/RicoSuter/NSwag/issues/1991
    // Issue when generating properties that aren't required.
    class SafeContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProp = base.CreateProperty(member, memberSerialization);
            jsonProp.Required = Required.Default;
            return jsonProp;
        }
    }
}
