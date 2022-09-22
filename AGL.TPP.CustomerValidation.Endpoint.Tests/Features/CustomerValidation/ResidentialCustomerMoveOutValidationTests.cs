namespace AGL.TPP.CustomerValidation.Endpoint.Tests.Features.ResidentialCustomerValidation
{
    using System.Net;
    using AGL.TPP.CustomerValidation.API.Models;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.Fixtures;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.TestData.Builders;
    using TestStack.BDDfy;
    using Xunit;

    [Collection(FixtureCollections.TestServerFixtureCollection)]
    public class ResidentialCustomerMoveOutValidationTests
    {
        private readonly ResidentialCustomerMoveOutValidationSteps _steps;

        public ResidentialCustomerMoveOutValidationTests(TestServerFixture fixture)
        {
            _steps = new ResidentialCustomerMoveOutValidationSteps(fixture);
            fixture.Reset();
        }

        [Fact]
        public void StatusHeaderKeyNotPresentReturnsBadRequest()
        {
            this.When(s => _steps.WhenTheRequestExecutes("/residential/moveOut", new ResidentialCustomerSalesValidationModel()))
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
                    "/residential/moveOut", 
                    new ResidentialCustomerMoveOutValidationBuilder()
                    .ForTransactionType("MoveOut")
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
                    "/residential/moveOut", 
                    new ResidentialCustomerMoveOutValidationBuilder()
                    .ForTransactionType("MoveOut")
                    .RequestWithInvalidResidentialCustomerValidationData()
                    .Build()))
                .Then(s => _steps.TheResponseCodeIs(HttpStatusCode.Unauthorized))
                .And(s => _steps.TheReturnedContentModelIs(response))
                .BDDfy();
        }
    }
}
