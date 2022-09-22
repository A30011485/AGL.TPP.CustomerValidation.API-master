using Newtonsoft.Json;

namespace AGL.TPP.CustomerValidation.API.Tests
{
    public class JsonDataReader
    {
        public static T GetData<T>(string fileName) where T : class
        {
            var data = JsonConvert.DeserializeObject<T>(System.IO.File.ReadAllText($@"TestData/{fileName}.json"));

            return data;
        }
    }
}
