using System.Text.Json;

namespace Ajupov.Utils.All.Json
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new (JsonSerializerDefaults.Web);

        public static T FromJsonString<T>(this string value)
        {
            return JsonSerializer.Deserialize<T>(value, JsonSerializerOptions);
        }

        public static string ToJsonString(this object value)
        {
            return JsonSerializer.Serialize(value, JsonSerializerOptions);
        }
    }
}
