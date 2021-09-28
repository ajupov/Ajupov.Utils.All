using System.Collections.Generic;
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
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            var fullUri = GetFullUri(uri, parameters);

            using var client = factory.CreateClient();

            var result = await client
                .AddHeaders(headers)
                .GetAsync(fullUri, ct);

            result.EnsureSuccessStatusCode();
        }

        public static async Task<TResponse> GetAsync<TResponse>(
            this IHttpClientFactory factory,
            string uri,
            object parameters = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            var fullUri = GetFullUri(uri, parameters);

            using var client = factory.CreateClient();

            var result = await client
                .AddHeaders(headers)
                .GetAsync(fullUri, ct);

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync(ct);

            return content.FromJsonString<TResponse>();
        }

        public static async Task PostJsonAsync(
            this IHttpClientFactory factory,
            string uri,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = factory.CreateClient();

            var result = await client
                .AddHeaders(headers)
                .PostAsync(GetFullUri(uri), body.ToJsonStringContent(), ct);

            result.EnsureSuccessStatusCode();
        }

        public static async Task<TResponse> PostJsonAsync<TResponse>(
            this IHttpClientFactory factory,
            string uri,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = factory.CreateClient();

            var result = await client
                .AddHeaders(headers)
                .PostAsync(GetFullUri(uri), body.ToJsonStringContent(), ct);

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync(ct);

            return content.FromJsonString<TResponse>();
        }

        public static async Task PutJsonAsync(
            this IHttpClientFactory factory,
            string uri,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = factory.CreateClient();

            var result = await client
                .AddHeaders(headers)
                .PutAsync(GetFullUri(uri), body.ToJsonStringContent(), ct);

            result.EnsureSuccessStatusCode();
        }

        public static async Task<TResponse> PutJsonAsync<TResponse>(
            this IHttpClientFactory factory,
            string uri,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = factory.CreateClient();

            var result = await client
                .AddHeaders(headers)
                .PutAsync(GetFullUri(uri), body.ToJsonStringContent(), ct);

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync(ct);

            return content.FromJsonString<TResponse>();
        }

        public static async Task PatchJsonAsync(
            this IHttpClientFactory factory,
            string uri,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = factory.CreateClient();

            var result = await client
                .AddHeaders(headers)
                .PatchAsync(GetFullUri(uri), body.ToJsonStringContent(), ct);

            result.EnsureSuccessStatusCode();
        }

        public static async Task<TResponse> PatchJsonAsync<TResponse>(
            this IHttpClientFactory factory,
            string uri,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = factory.CreateClient();

            var result = await client
                .AddHeaders(headers)
                .PatchAsync(GetFullUri(uri), body.ToJsonStringContent(), ct);

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync(ct);

            return content.FromJsonString<TResponse>();
        }

        public static async Task DeleteJsonAsync(
            this IHttpClientFactory factory,
            string uri,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = factory.CreateClient();

            var result = await client
                .AddHeaders(headers)
                .DeleteAsync(GetFullUri(uri), ct);

            result.EnsureSuccessStatusCode();
        }

        public static async Task PostFormDataAsync(
            this IHttpClientFactory factory,
            string uri,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = factory.CreateClient();

            var result = await client
                .AddHeaders(headers)
                .PostAsync(GetFullUri(uri), body.ToFormDataContent(), ct);

            result.EnsureSuccessStatusCode();
        }

        public static async Task<TResponse> PostFormDataAsync<TResponse>(
            this IHttpClientFactory factory,
            string uri,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = factory.CreateClient();

            var result = await client
                .AddHeaders(headers)
                .PostAsync(GetFullUri(uri), body.ToFormDataContent(), ct);

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync(ct);

            return content.FromJsonString<TResponse>();
        }

        private static string GetFullUri(string uri, object parameters = null)
        {
            return $"{uri}{parameters.ToQueryParams()}";
        }
    }
}
