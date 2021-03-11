using System.Text.Json;

namespace Ajupov.Utils.All.Json
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerOptions Options = new ()
        {
            PropertyNameCaseInsensitive = true
        };

        public static T FromJsonString<T>(this string value)
        {
            return JsonSerializer.Deserialize<T>(value, Options);
        }

        public static string ToJsonString(this object value)
        {
            return JsonSerializer.Serialize(value, Options);
        }
    }
}
