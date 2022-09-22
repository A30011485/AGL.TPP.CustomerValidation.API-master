namespace AGL.TPP.CustomerValidation.Endpoint.Tests.Features.ResidentialCustomerCancellation
{
    using System.Net;
    using AGL.TPP.CustomerValidation.API.Models;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.Fixtures;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.TestData.Builders;
    using TestStack.BDDfy;
    using Xunit;

    [Collection(FixtureCollections.TestServerFixtureCollection)]
    public class ResidentialCustomerCancellationTests
    {
        private readonly ResidentialCustomerCancellationSteps _steps;

        public ResidentialCustomerCancellationTests(TestServerFixture fixture)
        {
            _steps = new ResidentialCustomerCancellationSteps(fixture);
            fixture.Reset();
        }

        [Fact]
        public void StatusHeaderKeyNotPresentReturnsBadRequest()
        {
            this.When(s => _steps.WhenTheRequestExecutes("/residential/cancellation", new ResidentialCustomerSalesValidationModel()))
                .Then(s => _steps.TheResponseCodeIs(HttpStatusCode.Unauthorized))
                .BDDfy();
        }

        [Theory]
        [InlineData(true)]
        public void ResidentialCustomerCancellationValidationIsSuccessful(bool isHealthy)
        {
            CustomerValidationResponse response = null;

            this.Given(s => _steps.AnAuthenticatedUser())
                .And(s => _steps.GivenTheStatusKeyHeaderExists())
                .And(s => _steps.GivenPingReturns(isHealthy))
                .When(s => _steps.WhenTheRequestExecutes(
                    "/residential/cancellation", 
                    new ResidentialCustomerCancellationBuilder()
                    .ForTransactionType("Cancel")
                    .RequestWithValidResidentialCustomerCancellationData()
                    .Build()))
                .Then(s => _steps.TheResponseCodeIs(HttpStatusCode.Unauthorized))
                .And(s => _steps.TheReturnedContentModelIs(response))
                .BDDfy();
        }

        [Theory]
        [InlineData(true)]
        public void ResidentialCustomerCancellationValidationFails(bool isHealthy)
        {
            CustomerValidationResponse response = null;

            this.Given(s => _steps.AnAuthenticatedUser())
                .And(s => _steps.GivenTheStatusKeyHeaderExists())
                .And(s => _steps.GivenPingReturns(isHealthy))
                .When(s => _steps.WhenTheRequestExecutes(
                    "/residential/cancellation", 
                    new ResidentialCustomerCancellationBuilder()
                    .ForTransactionType("Cancel")
                    .RequestWithInvalidResidentialCustomerCancellationData()
                    .Build()))
                .Then(s => _steps.TheResponseCodeIs(HttpStatusCode.Unauthorized))
                .And(s => _steps.TheReturnedContentModelIs(response))
                .BDDfy();
        }
    }
}
