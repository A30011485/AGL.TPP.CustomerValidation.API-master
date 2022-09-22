using System.Net;
using AGL.TPP.CustomerValidation.API.Models;
using TestStack.BDDfy;
using Xunit;

namespace AGL.TPP.CustomerValidation.EndToEnd.Integration.Tests.Features
{
    [Story(AsA = "As a user",
           IWant = "I want to ensure Table storage connectivity",
           SoThat = "I can send the sales order for prcoessing")]
    public class StatusTests
    {
        private readonly StatusSteps _steps;

        public StatusTests()
        {
            _steps = new StatusSteps();
        }

        [Fact]
        public void CustomerValidationStatus()
        {
            var expectedResponse = new HealthCheckResponse()
            {
                IsHealthy = true
            };

            this.Given(_ => _steps.GivenCorrectHeadersExists())
                .When(_ => _steps.WhenTheRequestExecutes(_steps.CustomerValidationBaseUrl + "status"))
                .Then(_ => _steps.TheResponseCodeIs(HttpStatusCode.OK))
                .And(_ => _steps.ContentIsValid(expectedResponse))
                .BDDfy<StatusTests>();
        }
    }
}
