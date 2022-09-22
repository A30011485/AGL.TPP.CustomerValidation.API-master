using System;

namespace AGL.TPP.CustomerValidation.API.Helpers
{
    public static class DateTimeExtensions
    {
        private static readonly TimeZoneInfo AustralianEasternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");

        public static DateTime CurrentAestDateTime(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, AustralianEasternTimeZone);
        }

        public static DateTime ToEndOfDay(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddSeconds(-1);
        }
    }
}
