namespace Ajupov.Utils.All.String
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string value)
        {
            return value == null || string.IsNullOrWhiteSpace(value);
        }
    }
}