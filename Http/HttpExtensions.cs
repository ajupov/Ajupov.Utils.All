using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Web;
using Ajupov.Utils.All.Json;

namespace Ajupov.Utils.All.Http
{
    public static class HttpExtensions
    {
        public static string ToQueryParams(this object parameters)
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

            return result.Any() ? $"?{string.Join("&", result)}" : string.Empty;
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
            if (headers == null)
            {
                return client;
            }

            foreach (var (key, value) in headers)
            {
                client.DefaultRequestHeaders.Add(key, value);
            }

            return client;
        }

        public static StringContent ToJsonStringContent(this object model)
        {
            return new StringContent(model.ToJsonString(), Encoding.UTF8, "application/json");
        }

        public static FormUrlEncodedContent ToFormDataContent(this object model)
        {
            return new FormUrlEncodedContent(
                model?
                    .GetType()
                    .GetProperties()
                    .Select(x =>
                    {
                        var jsonPropertyValue = x.CustomAttributes
                            .FirstOrDefault(a => a.AttributeType == typeof(JsonPropertyNameAttribute))?
                            .ConstructorArguments.FirstOrDefault().Value?.ToString();

                        return new KeyValuePair<string, string>(
                            !string.IsNullOrEmpty(jsonPropertyValue) ? jsonPropertyValue : x.Name,
                            x.GetValue(model)?.ToString());
                    })
                ?? Array.Empty<KeyValuePair<string, string>>());
        }
    }
}
