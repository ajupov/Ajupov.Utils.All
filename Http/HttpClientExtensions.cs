using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ajupov.Utils.All.Json;

namespace Ajupov.Utils.All.Http
{
    public static class HttpClientExtensions
    {
        public static async Task GetAsync(
            this IHttpClientFactory factory,
            string uri,
            object parameters = default,
            CancellationToken ct = default)
        {
            var fullUri = GetFullUri(uri, parameters);

            using var client = factory.CreateClient();
            var result = await client.GetAsync(fullUri, ct);

            result.EnsureSuccessStatusCode();
        }

        public static async Task<TResponse> GetAsync<TResponse>(
            this IHttpClientFactory factory,
            string uri,
            object parameters = default,
            CancellationToken ct = default)
        {
            var fullUri = GetFullUri(uri, parameters);

            using var client = factory.CreateClient();
            var result = await client.GetAsync(fullUri, ct);

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            return content.FromJsonString<TResponse>();
        }

        public static async Task PostAsync(
            this IHttpClientFactory factory,
            string uri,
            object body = default,
            CancellationToken ct = default)
        {
            var fullUri = GetFullUri(uri);

            using var client = factory.CreateClient();
            var result = await client.PostAsync(fullUri, body.ToJsonStringContent(), ct);

            result.EnsureSuccessStatusCode();
        }

        public static async Task<TResponse> PostAsync<TResponse>(
            this IHttpClientFactory factory,
            string uri,
            object body = default,
            CancellationToken ct = default)
        {
            var fullUri = GetFullUri(uri);

            using var client = factory.CreateClient();
            var result = await client.PostAsync(fullUri, body.ToJsonStringContent(), ct);

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            return content.FromJsonString<TResponse>();
        }

        private static string GetFullUri(string uri, object parameters = null)
        {
            return $"{uri}{parameters.ToQueryParams()}";
        }
    }
}