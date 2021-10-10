using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Ajupov.Utils.All.Json;

namespace Ajupov.Utils.All.Http
{
    public static class HttpExtensions
    {
        public static string AddParameters(this string uri, object parameters)
        {
            var properties = TypeDescriptor.GetProperties(parameters);
            var result = new List<string>();

            foreach (PropertyDescriptor property in properties)
            {
                if (property == null)
                {
                    continue;
                }

                var value = property.GetValue(parameters);
                if (value is IEnumerable enumerable && !(value is string))
                {
                    var items = enumerable.Cast<object>().Select(x => x.ToString());

                    result.AddRange(items.Select(x => $"{property.Name}={x}"));
                }
                else
                {
                    result.Add($"{property.Name}={value}");
                }
            }

            return uri + (result.Any() ? $"?{string.Join("&", result)}" : string.Empty);
        }

        public static string AddParameters(this string uri, params (string key, object value)[] parameters)
        {
            var uriBuilder = new System.UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (var (key, value) in parameters)
            {
                query[key] = value.ToString();
            }

            uriBuilder.Query = query.ToString() ?? string.Empty;

            return uriBuilder.ToString();
        }

        public static HttpClient AddHeaders(this HttpClient client, Dictionary<string, string> headers)
        {
            foreach (var (key, value) in headers ?? new Dictionary<string, string>())
            {
                client.DefaultRequestHeaders.Add(key, value);
            }

            return client;
        }

        public static HttpContent ToStringContent(this object body)
        {
            return new StringContent(body.ToJsonString(), Encoding.UTF8, "application/json");
        }

        public static HttpContent ToFormUrlEncodedContent(this object body)
        {
            return new FormUrlEncodedContent(
                body?
                    .GetType()
                    .GetProperties()
                    .Select(x =>
                    {
                        var jsonPropertyValue = x.CustomAttributes
                            .FirstOrDefault(a => a.AttributeType == typeof(JsonPropertyNameAttribute))?
                            .ConstructorArguments.FirstOrDefault().Value?.ToString();

                        return new KeyValuePair<string, string>(
                            !string.IsNullOrEmpty(jsonPropertyValue) ? jsonPropertyValue : x.Name,
                            x.GetValue(body)?.ToString());
                    })
                ?? Array.Empty<KeyValuePair<string, string>>());
        }

        public static async Task<TResult> ReadResponseContentAsync<TResult>(
            this HttpResponseMessage response,
            CancellationToken ct)
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync(ct);

            return content.FromJsonString<TResult>();
        }
    }
}
