namespace Ajupov.Utils.All.Phone
{
    public class PhoneSettings
    {
        public const string Plus = "+";

        public PhoneSettings(string country, int length, string internationalPrefix, string innerPrefix)
        {
            Country = country;
            Length = length;
            InternationalPrefix = internationalPrefix;
            InnerPrefix = innerPrefix;
        }

        public string Country { get; }

        public int Length { get; }

        public string InternationalPrefix { get; }

        public string InnerPrefix { get; }
    }
}