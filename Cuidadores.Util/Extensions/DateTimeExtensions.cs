using System;

namespace Cuidadores.Util.Extensions
{
    public static class DateTimeExtensions
    {
        private const string DefaultDateFormat = "d";
        private const string DefaultDateTimeFormat = "G";
        private const string DefaultDateTimeFriendlyTemplate = "{0:d} às {1:t}";
        private const string DefaultDateFriendlyTemplate = "{0:d}";

        public static string ToStringDate(this DateTime dateTime)
        {
            return ToStringDate(dateTime, DefaultDateFormat);
        }

        public static string ToStringDate(this DateTime dateTime, string format)
        {
            return dateTime.ToString(format);
        }

        public static string ToStringDate(this DateTime? dateTime)
        {
            return ToStringDate(dateTime, DefaultDateFormat);
        }

        public static string ToStringDate(this DateTime? dateTime, string format)
        {
            if (dateTime == null)
            {
                return null;
            }
            return ToStringDate(dateTime.Value, format);
        }

        public static string ToStringDateTime(this DateTime dateTime)
        {
            return ToStringDate(dateTime, DefaultDateTimeFormat);
        }

        public static string ToStringDateTime(this DateTime dateTime, string format)
        {
            return dateTime.ToString(format);
        }

        public static string ToStringDateTime(this DateTime? dateTime)
        {
            return ToStringDate(dateTime, DefaultDateTimeFormat);
        }

        public static string ToStringDateTime(this DateTime? dateTime, string format)
        {
            if (dateTime == null)
            {
                return null;
            }
            return ToStringDate(dateTime.Value, format);
        }

        public static string ToStringDateTimeFriendly(this DateTime dateTime)
        {
            return string.Format(DefaultDateTimeFriendlyTemplate, dateTime, dateTime);
        }

        public static string ToStringDateFriendly(this DateTime dateTime)
        {
            return (dateTime.Year < 1990) ? "---" : string.Format(DefaultDateFriendlyTemplate, dateTime, dateTime);
        }
        public static string ToStringDateFriendly(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return null;
            }
            return ToStringDateFriendly(dateTime.Value);
        }

        public static string ToStringDateTimeFriendly(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return null;
            }
            return ToStringDateTimeFriendly(dateTime.Value);
        }
    }
}
