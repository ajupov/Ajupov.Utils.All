using Newtonsoft.Json;

namespace Ajupov.Utils.All.Json
{
    public static class JsonExtensions
    {
        public static T FromJsonString<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static string ToJsonString(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}