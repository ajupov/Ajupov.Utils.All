using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Ajupov.Utils.All.Json
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static T FromJsonString<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value, Settings);
        }

        public static string ToJsonString(this object value)
        {
            return JsonConvert.SerializeObject(value, Settings);
        }
    }
}