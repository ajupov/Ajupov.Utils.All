using System;
using System.Globalization;
using SystemDateTime = System.DateTime;

namespace Infrastructure.All.DateTime
{
    public static class DateTimeExtensions
    {
        private const string DefaultDateFormat = "dd.MM.yyyy";
        private const string DefaultTimeFormat = "HH:mm:ss";
        private const string DefaultDateTimeFormat = "dd.MM.yyyy HH:mm:ss";

        public static string ToDateString(this SystemDateTime? value, string format = DefaultDateFormat)
        {
            return value?.ToString(format);
        }

        public static string ToTimeString(this SystemDateTime? value, string format = DefaultTimeFormat)
        {
            return value?.ToString(format);
        }

        public static string ToDateTimeString(this SystemDateTime? value, string format = DefaultDateTimeFormat)
        {
            return value?.ToString(format);
        }

        public static SystemDateTime? ToDate(this string value, string format = DefaultDateFormat)
        {
            return SystemDateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var date)
                ? (SystemDateTime?) date
                : null;
        }

        public static SystemDateTime? ToTime(this string value, string format = DefaultTimeFormat)
        {
            return SystemDateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var time)
                ? (SystemDateTime?) time
                : null;
        }

        public static SystemDateTime? ToDateTime(this string value, string format = DefaultDateTimeFormat)
        {
            return SystemDateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var dateTime)
                ? (SystemDateTime?) dateTime
                : null;
        }

        public static string ToWeekName(this DayOfWeek dayOfWeek)
        {
            return DateTimeFormatInfo.CurrentInfo?.GetDayName(dayOfWeek);
        }

        public static string ToMonthName(this int month)
        {
            return DateTimeFormatInfo.CurrentInfo?.GetMonthName(month);
        }

        public static SystemDateTime FirstDayOfYear(this SystemDateTime value)
        {
            return new SystemDateTime(value.Year, 1, 1);
        }

        public static SystemDateTime FirstDayOfMonth(this SystemDateTime value)
        {
            return new SystemDateTime(value.Year, value.Month, 1);
        }

        public static bool IsMoreThanMinValue(this SystemDateTime value)
        {
            return value > SystemDateTime.MinValue;
        }

        public static bool IsLessThanMaxValue(this SystemDateTime value)
        {
            return value < SystemDateTime.MaxValue;
        }
    }
}