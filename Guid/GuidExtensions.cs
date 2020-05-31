namespace Ajupov.Utils.All.Guid
{
    public static class GuidExtensions
    {
        public static bool IsEmpty(this System.Guid? value)
        {
            return !value.HasValue || IsEmpty(value.Value);
        }

        public static bool IsEmpty(this System.Guid value)
        {
            return value == System.Guid.Empty;
        }
    }
}
