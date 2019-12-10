using System.Collections.Generic;

namespace Ajupov.Utils.All.Phone
{
    public static class PhoneExtensions
    {
        public static readonly Dictionary<string, PhoneSettings> Settings = new Dictionary<string, PhoneSettings>
        {
            {Country.CountryName.Russia, new PhoneSettings(Country.CountryName.Russia, 10, "7", "8")}
        };

        public static string GetInternationalPhonePrefix(this string country)
        {
            return Settings[country].InternationalPrefix;
        }

        public static string GetInnerPhonePrefix(this string country)
        {
            return Settings[country].InnerPrefix;
        }

        public static string GetFullInternationalPhonePrefix(this string country)
        {
            return $"{PhoneSettings.Plus}{GetInternationalPhonePrefix(country)}";
        }

        public static string GetPhoneWithoutPrefixes(this string value, string country)
        {
            var settings = Settings[country];

            var fullInternationalPrefix = GetFullInternationalPhonePrefix(country);
            if (value.Length == settings.Length + fullInternationalPrefix.Length &&
                value.StartsWith(fullInternationalPrefix))
            {
                return value.Substring(fullInternationalPrefix.Length);
            }

            var internationalPrefix = GetInternationalPhonePrefix(country);
            if (value.Length == settings.Length + internationalPrefix.Length && value.StartsWith(internationalPrefix))
            {
                return value.Substring(internationalPrefix.Length);
            }

            var innerPrefix = GetInnerPhonePrefix(country);
            if (value.Length == settings.Length + innerPrefix.Length && value.StartsWith(innerPrefix))
            {
                return value.Substring(innerPrefix.Length);
            }

            return value;
        }
    }
}