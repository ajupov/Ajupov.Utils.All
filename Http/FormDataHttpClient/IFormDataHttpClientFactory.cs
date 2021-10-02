using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ajupov.Utils.All.Http.FormDataHttpClient
{
    public interface IFormDataHttpClientFactory
    {
        Task<TResult> GetAsync<TResult>(
            string uri,
            object parameters = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default);

        Task<TResult> PostAsync<TResult>(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default);

        Task<TResult> PutAsync<TResult>(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default);

        Task<TResult> PatchAsync<TResult>(
            string uri,
            object parameters = default,
            object body = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default);

        Task<TResult> DeleteAsync<TResult>(
            string uri,
            object parameters = default,
            Dictionary<string, string> headers = default,
            CancellationToken ct = default);
    }
}
