namespace AGL.TPP.CustomerValidation.API.Extensions.Web
{
    using System.Linq;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Http Request extensions
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Returns an individual HTTP Header value
        /// </summary>
        /// <param name="request">Http Request to be searched for</param>
        /// <param name="key">Key to find in headers</param>
        /// <param name="defaultValue">Default value to be if header not found</param>
        /// <returns></returns>
        public static string GetRequestHeaderOrDefault(this HttpRequest request, string key, string defaultValue = null)
        {
            var value = request?.Headers?.FirstOrDefault(_ => _.Key.Equals(key)).Value.FirstOrDefault();

            if(string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            return value;
        }

        /// <summary>
        /// Returns an individual HTTP Header value
        /// </summary>
        /// <param name="headers">Http headers to be searched for</param>
        /// <param name="key">Key to find in headers</param>
        /// <param name="defaultValue">Default value to be if header not found</param>
        /// <returns></returns>
        public static string GetHeaderOrDefault(this IHeaderDictionary headers, string key, string defaultValue = null)
        {
            var value = headers?.FirstOrDefault(_ => _.Key.Equals(key)).Value.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            return value;
        }
    }
}