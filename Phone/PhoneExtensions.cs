using System.Linq;

namespace Ajupov.Utils.All.Phone
{
    public static class PhoneExtensions
    {
        private const int DefaultMaxLength = 10;

        public static string ExtractPhone(this string value, int maxLength = DefaultMaxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var result = new string(value.ToCharArray().Where(char.IsDigit).ToArray());

            return result.Substring(result.Length - maxLength);
        }
    }
}