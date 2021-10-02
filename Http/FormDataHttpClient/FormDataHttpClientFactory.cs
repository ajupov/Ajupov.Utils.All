using System.Collections.Generic;
using System.Net.Http;
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

        public Task GetAsync(
            string uri,
            object parameters = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            return GetInternalAsync(uri, parameters, headers, ct);
        }

        public async Task<T> GetAsync<T>(
            string uri,
            object parameters = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            var response = await GetInternalAsync(uri, parameters, headers, ct);

            return await response.ReadResponseContentAsync<T>(ct);
        }

        public Task PostAsync(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            return PostInternalAsync(uri, parameters, body, headers, ct);
        }

        public async Task<T> PostAsync<T>(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            var response = await PostInternalAsync(uri, parameters, body, headers, ct);

            return await response.ReadResponseContentAsync<T>(ct);
        }

        public Task PutAsync(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            return PutInternalAsync(uri, parameters, body, headers, ct);
        }

        public async Task<T> PutAsync<T>(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            var response = await PutInternalAsync(uri, parameters, body, headers, ct);

            return await response.ReadResponseContentAsync<T>(ct);
        }

        public Task PatchAsync(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            return PathInternalAsync(uri, parameters, body, headers, ct);
        }

        public async Task<T> PatchAsync<T>(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            var response = await PathInternalAsync(uri, parameters, body, headers, ct);

            return await response.ReadResponseContentAsync<T>(ct);
        }

        public Task DeleteAsync(
            string uri,
            object parameters = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            return DeleteInternalAsync(uri, parameters, headers, ct);
        }

        public async Task<T> DeleteAsync<T>(
            string uri,
            object parameters = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            var response = await DeleteInternalAsync(uri, parameters, headers, ct);

            return await response.ReadResponseContentAsync<T>(ct);
        }

        private async Task<HttpResponseMessage> GetInternalAsync(
            string uri,
            object parameters = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = _factory.CreateClient();

            var response = await client
                .AddHeaders(headers)
                .GetAsync(uri.AddParameters(parameters), ct);

            response.EnsureSuccessStatusCode();

            return response;
        }

        private async Task<HttpResponseMessage> PostInternalAsync(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = _factory.CreateClient();

            var response = await client
                .AddHeaders(headers)
                .PostAsync(uri.AddParameters(parameters), body.ToFormUrlEncodedContent(), ct);

            response.EnsureSuccessStatusCode();

            return response;
        }

        private async Task<HttpResponseMessage> PutInternalAsync(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = _factory.CreateClient();

            var response = await client
                .AddHeaders(headers)
                .PutAsync(uri.AddParameters(parameters), body.ToFormUrlEncodedContent(), ct);

            response.EnsureSuccessStatusCode();

            return response;
        }

        private async Task<HttpResponseMessage> PathInternalAsync(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = _factory.CreateClient();

            var response = await client
                .AddHeaders(headers)
                .PatchAsync(uri.AddParameters(parameters), body.ToFormUrlEncodedContent(), ct);

            response.EnsureSuccessStatusCode();

            return response;
        }

        private async Task<HttpResponseMessage> DeleteInternalAsync(
            string uri,
            object parameters = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default)
        {
            using var client = _factory.CreateClient();

            var response = await client
                .AddHeaders(headers)
                .DeleteAsync(uri.AddParameters(parameters), ct);

            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}
