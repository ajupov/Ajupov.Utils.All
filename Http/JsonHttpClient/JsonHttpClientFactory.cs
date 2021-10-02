using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Ajupov.Utils.All.Http.JsonHttpClient
{
    public class JsonHttpClientFactory : IJsonHttpClientFactory
    {
        private readonly IHttpClientFactory _factory;

        public JsonHttpClientFactory(IHttpClientFactory factory)
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
            var response = await client.PostAsync(uri, ToStringContent(body), ct);

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
            var response = await client.PutAsync(uri, ToStringContent(body), ct);

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
            var response = await client.PatchAsync(uri, ToStringContent(body), ct);

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

        private static HttpContent ToStringContent(object body)
        {
            return new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json; charset=UTF-8");
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
