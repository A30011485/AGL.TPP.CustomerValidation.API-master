using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace AGL.TPP.CustomerValidation.Endpoint.Tests.Extensions
{
    public static class StringExtensions
    {
        public static List<string> SplitSemicolonSeperatedValues(this string commaSeperatedString)
        {
            var values = new List<string>();
            if (!string.IsNullOrEmpty(commaSeperatedString))
                values = commaSeperatedString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim())
                    .Where(x => !string.IsNullOrEmpty(x)).ToList();
            return values;
        }

        public static byte[] GetBytes(this string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            return Encoding.UTF8.GetBytes(str);
        }

        public static string GetString(this byte[] bytes)
        {
            return bytes != null ? Encoding.UTF8.GetString(bytes) : null;
        }

        public static string ReadToString(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static byte[] ToByteArray(this Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static string StripWhitespace(this string input)
        {
            return input?.Replace(" ", "");
        }

        public static bool EqualsCaseInsensitive(this string input, string compareTo)
        {
            if (input == null && compareTo == null)
                return true;

            return input != null &&
                   input.Equals(compareTo, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool ContainsNumbersOnly(this string input)
        {
            return !string.IsNullOrEmpty(input)
                   && input.All(a => Char.IsDigit(a));
        }

        public static string ToJsonMessage(this string message)
        {
            return $"{{\"message\": \"{message}\"}}";
        }

        public static object FromJsonToObject(this string jsonString)
        {
            return JsonConvert.DeserializeObject(jsonString);
        }

        public static T FromJsonToObject<T>(this string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}