using System.Net;
using AGL.TPP.CustomerValidation.API.Models;
using AGL.TPP.CustomerValidation.Endpoint.Tests.Fixtures;
using TestStack.BDDfy;
using Xunit;

namespace AGL.TPP.CustomerValidation.Endpoint.Tests.Features.Status
{
    [Collection(FixtureCollections.TestServerFixtureCollection)]
    public class StatusTests
    {
        private readonly StatusSteps _steps;

        public StatusTests(TestServerFixture fixture)
        {
            _steps = new StatusSteps(fixture);
            fixture.Reset();
        }

        [Fact]
        public void StatusHeaderKeyNotPresentReturnsBadRequest()
        {
            this.When(s => _steps.WhenTheRequestExecutes("/status"))
                .Then(s => _steps.TheResponseCodeIs(HttpStatusCode.BadRequest))
                .And(s => _steps.TheReturnedContentIs(new CustomerValidationResponse(Constants.ErrorCode.InvalidStatusCheckKey.ToString(),
                    Constants.ErrorMessages.StatusClientKeyHeaderInvalid)))
                .BDDfy();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CallStatusEndpointReturnsHealthy(bool status)
        {
            this.When(_ => _steps.GivenPingReturns(status))
                .And(_ => _steps.GivenTheStatusKeyHeaderExists())
                .And(s => _steps.WhenTheRequestExecutes("/status"))
                .Then(s => _steps.TheResponseCodeIs(HttpStatusCode.OK))
                .And(s => _steps.TheReturnedContentIs(new HealthCheckResponse { IsHealthy = status }))
                .BDDfy();
        }
    }
}
