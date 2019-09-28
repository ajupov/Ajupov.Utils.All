namespace Ajupov.Utils.All.Http
{
    public static class UriBuilder
    {
        public static string Combine(string host, string resource, string action = null)
        {
            const char slash = '/';

            return $"{host.Trim(slash)}/{resource.Trim(slash)}{action?.Trim(slash)}";
        }
    }
}