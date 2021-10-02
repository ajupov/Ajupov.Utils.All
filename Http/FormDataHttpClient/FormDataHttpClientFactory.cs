using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Ajupov.Utils.All.Http.FormDataHttpClient
{
    public class FormDataHttpClientFactory : IFormDataHttpClientFactory
    {
        private readonly IHttpClientFactory _factory;

        public FormDataHttpClientFactory(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<TResult> GetAsync<TResult>(
            string uri,
            object parameters = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = _factory.CreateClient();

            AddHeaders(client, headers);
            var response = await client.GetAsync(uri, ct);

            return await ReadResponseContentAsync<TResult>(response, ct);
        }

        public async Task<TResult> PostAsync<TResult>(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = _factory.CreateClient();

            AddHeaders(client, headers);
            var response = await client.PostAsync(uri, ToFormUrlEncodedContent(body), ct);

            return await ReadResponseContentAsync<TResult>(response, ct);
        }

        public async Task<TResult> PutAsync<TResult>(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = _factory.CreateClient();

            AddHeaders(client, headers);
            var response = await client.PutAsync(uri, ToFormUrlEncodedContent(body), ct);

            return await ReadResponseContentAsync<TResult>(response, ct);
        }

        public async Task<TResult> PatchAsync<TResult>(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = _factory.CreateClient();

            AddHeaders(client, headers);
            var response = await client.PatchAsync(uri, ToFormUrlEncodedContent(body), ct);

            return await ReadResponseContentAsync<TResult>(response, ct);
        }

        public async Task<TResult> DeleteAsync<TResult>(
            string uri,
            object parameters = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = _factory.CreateClient();

            AddHeaders(client, headers);
            var response = await client.DeleteAsync(uri, ct);

            return await ReadResponseContentAsync<TResult>(response, ct);
        }

        private static void AddHeaders(HttpClient client, Dictionary<string, string> headers)
        {
            foreach (var (key, value) in headers ?? new Dictionary<string, string>())
            {
                client.DefaultRequestHeaders.Add(key, value);
            }
        }

        private static HttpContent ToFormUrlEncodedContent(object body)
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

        private static async Task<TResult> ReadResponseContentAsync<TResult>(
            HttpResponseMessage response,
            CancellationToken ct)
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync(ct);

            return typeof(TResult) == typeof(void) ? default : JsonSerializer.Deserialize<TResult>(content);
        }
    }
}
