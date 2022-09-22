namespace AGL.TPP.CustomerValidation.Endpoint.Tests.Features.ResidentialCustomerValidation
{
    using System.Net;
    using AGL.TPP.CustomerValidation.API.Models;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.Fixtures;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.TestData.Builders;
    using TestStack.BDDfy;
    using Xunit;

    [Collection(FixtureCollections.TestServerFixtureCollection)]
    public class ResidentialCustomerValidationTests
    {
        private readonly ResidentialCustomerValidationSteps _steps;

        public ResidentialCustomerValidationTests(TestServerFixture fixture)
        {
            _steps = new ResidentialCustomerValidationSteps(fixture);
            fixture.Reset();
        }

        [Fact]
        public void StatusHeaderKeyNotPresentReturnsBadRequest()
        {
            this.When(s => _steps.WhenTheRequestExecutes("/residential/sales", new ResidentialCustomerSalesValidationModel()))
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
                    "/residential/sales", 
                    new ResidentialCustomerValidationBuilder()
                    .ForTransactionType("Sale")
                    .RequestWithValidResidentialCustomerValidationData()
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
                    "/residential/sales", 
                    new ResidentialCustomerValidationBuilder()
                    .ForTransactionType("Sale")
                    .RequestWithInvalidResidentialCustomerValidationData()
                    .Build()))
                .Then(s => _steps.TheResponseCodeIs(HttpStatusCode.Unauthorized))
                .And(s => _steps.TheReturnedContentModelIs(response))
                .BDDfy();
        }
    }
}
