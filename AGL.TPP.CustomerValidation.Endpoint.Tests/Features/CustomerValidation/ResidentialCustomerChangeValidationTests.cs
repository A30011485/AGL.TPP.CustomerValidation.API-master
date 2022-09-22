namespace AGL.TPP.CustomerValidation.Endpoint.Tests.Features.ResidentialCustomerValidation
{
    using System.Net;
    using AGL.TPP.CustomerValidation.API.Models;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.Fixtures;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.TestData.Builders;
    using TestStack.BDDfy;
    using Xunit;

    [Collection(FixtureCollections.TestServerFixtureCollection)]
    public class ResidentialCustomerChangeValidationTests
    {
        private readonly ResidentialCustomerChangeValidationSteps _steps;

        public ResidentialCustomerChangeValidationTests(TestServerFixture fixture)
        {
            _steps = new ResidentialCustomerChangeValidationSteps(fixture);
            fixture.Reset();
        }

        [Fact]
        public void StatusHeaderKeyNotPresentReturnsBadRequest()
        {
            this.When(s => _steps.WhenTheRequestExecutes("/residential/change", new ResidentialCustomerChangeValidationModel()))
                .Then(s => _steps.TheResponseCodeIs(HttpStatusCode.Unauthorized))
                .BDDfy();
        }

        [Theory]
        [InlineData(true)]
        public void ResidentialCustomerValidationIsSuccessful(bool isHealthy)
        {
            CustomerValidationResponse response = null;

            this.Given(s => _steps.AnAuthenticatedUser())
                .And(s => _steps.GivenTheStatusKeyHeaderExists())
                .And(s => _steps.GivenPingReturns(isHealthy))
                .When(s => _steps.WhenTheRequestExecutes(
                    "/residential/change", 
                    new ResidentialCustomerChangeValidationBuilder()
                    .ForTransactionType("Change")
                    .RequestWithValidResidentialCustomerChangeValidationData()
                    .Build()))
                .Then(s => _steps.TheResponseCodeIs(HttpStatusCode.Unauthorized))
                .And(s => _steps.TheReturnedContentModelIs(response))
                .BDDfy();
        }

        [Theory]
        [InlineData(true)]
        public void ResidentialCustomerValidationFails(bool isHealthy)
        {
            CustomerValidationResponse response = null;

            this.Given(s => _steps.AnAuthenticatedUser())
                .And(s => _steps.GivenTheStatusKeyHeaderExists())
                .And(s => _steps.GivenPingReturns(isHealthy))
                .When(s => _steps.WhenTheRequestExecutes(
                    "/residential/change", 
                    new ResidentialCustomerChangeValidationBuilder()
                    .ForTransactionType("Change")
                    .RequestWithInvalidResidentialCustomerChangeValidationData()
                    .Build()))
                .Then(s => _steps.TheResponseCodeIs(HttpStatusCode.Unauthorized))
                .And(s => _steps.TheReturnedContentModelIs(response))
                .BDDfy();
        }
    }
}
