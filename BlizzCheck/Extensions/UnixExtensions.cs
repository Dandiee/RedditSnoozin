using System;

namespace BlizzCheck.Extensions
{
    public static class UnixExtensions
    {
        public static DateTime ToDateTime(this long unixTimestamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(unixTimestamp).ToLocalTime();
            return dateTime;
        }


        public static string ToDateTimeString(this long unixTimestamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(unixTimestamp).ToLocalTime();
            return dateTime.ToString("yyyy-MM-dd HH:mm");
        }


        public static string ToShort(this string str, int maxLength = 5)
        {
            if (str.Length < maxLength)
                return str;

            return str.Substring(0, maxLength) + ".";
        }

        public static string ToKey(this DateTime dt) => $"{dt:yyyyMMdd}";

    }
}
