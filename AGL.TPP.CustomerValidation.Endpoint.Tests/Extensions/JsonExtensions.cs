using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AGL.TPP.CustomerValidation.Endpoint.Tests.Extensions
{
    public static class JsonExtensions
    {
        public static T DeserializeJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string SerializeJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string ToJson(this object o)
        {
            if (o == null)
                return string.Empty;

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            settings.Converters.Add(
                new StringEnumConverter());

            return JsonConvert.SerializeObject(o, settings);
        }
    }
}
