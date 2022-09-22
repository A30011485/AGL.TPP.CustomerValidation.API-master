using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Models;
using FluentAssertions;
using Newtonsoft.Json;

namespace AGL.TPP.CustomerValidation.EndToEnd.Integration.Tests.Features
{
    public class StatusSteps : TestStepBase
    {
        public StatusSteps()
        {
            HttpClient.DefaultRequestHeaders.Add("Status-Client-Key", StatusClientKey);
        }

        public async Task ContentIsValid(HealthCheckResponse obj)
        {
            var responseString = await ResponseMessage.Content.ReadAsStringAsync();
            var actualResponse = JsonConvert.DeserializeObject<HealthCheckResponse>(responseString);

            actualResponse.Should().BeEquivalentTo(obj);
        }
    }
}
