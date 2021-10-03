using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ajupov.Utils.All.Http.JsonHttpClient
{
    public interface IJsonHttpClientFactory
    {
        Task GetAsync(
            string uri,
            object parameters = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default);

        Task<T> GetAsync<T>(
            string uri,
            object parameters = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default);

        Task<T> PostAsync<T>(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default);

        Task PostAsync(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default);

        Task<T> PutAsync<T>(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default);

        Task PutAsync(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default);

        Task<T> PatchAsync<T>(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default);

        Task PatchAsync(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default);

        Task<T> DeleteAsync<T>(
            string uri,
            object parameters = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default);

        Task DeleteAsync(
            string uri,
            object parameters = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default);
    }
}
