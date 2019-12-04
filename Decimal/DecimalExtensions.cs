namespace Ajupov.Utils.All.Decimal
{
    public static class DecimalExtensions
    {
        public static bool IsEmpty(this decimal? value)
        {
            return !value.HasValue || IsEmpty(value.Value);
        }

        public static bool IsEmpty(this decimal value)
        {
            return value == default;
        }
    }
}