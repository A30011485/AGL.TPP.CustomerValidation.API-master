using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using AGL.TPP.CustomerValidation.Endpoint.Tests.Extensions;

namespace AGL.TPP.CustomerValidation.Endpoint.Tests.TestData
{
    public static class TestMessageHelper
    {
        public static DateTime ParseDayArgument(string dayPhrase)
        {
            var retDateTime = DateTime.MinValue;
            if (string.IsNullOrEmpty(dayPhrase)) return retDateTime;
            var regex = new Regex(@"(\d*) day(?:s)? in the (\bpast\b|\bfuture\b)?");
            var match = regex.Match(dayPhrase);
            if (match.Success)
                return match.Length > 0
                    ? GetDateTimeFromStepArgument(int.Parse(match.Groups[1].Value), match.Groups[2].Value)
                    : retDateTime;
            return ParseDateTime(dayPhrase);
        }

        public static DateTime ParseDateTime(string dateTime)
        {
            DateTime lastSent;
            if (!DateTime.TryParseExact(dateTime, "yyyy-MM-dd HHmmss",
                new CultureInfo("en-AU"), DateTimeStyles.None, out lastSent))
                throw new InvalidDataException("Cannot parse the last sent time");

            return lastSent;
        }

        public static DateTime GetDateTimeFromStepArgument(int days, string relDay)
        {
            return DateTime.Today.AddDays(relDay == "past" ? days * -1 : days);
        }

        public static byte[] GetBlobContent(string resourceName)
        {
            return GetResourceContent(resourceName).GetBytes();
        }

        public static string GetResourceContent(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNamePrefix = "AGL.TPP.CustomerValidation.Endpoint.Test.TestData";

            using (var stream = assembly.GetManifestResourceStream($"{resourceNamePrefix}.{resourceName}"))
            {
                if (stream == null) return string.Empty;
                using (var reader = new StreamReader(stream))
                {
                    var result = reader.ReadToEnd();
                    return result;
                }
            }
        }
    }
}
