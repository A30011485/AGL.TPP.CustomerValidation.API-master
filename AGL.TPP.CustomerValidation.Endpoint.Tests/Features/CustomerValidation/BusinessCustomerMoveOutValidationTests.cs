namespace AGL.TPP.CustomerValidation.Endpoint.Tests.Features.BusinessCustomerValidation
{
    using AGL.TPP.CustomerValidation.API.Models;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.Fixtures;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.TestData.Builders;
    using System.Net;
    using TestStack.BDDfy;
    using Xunit;

    [Collection(FixtureCollections.TestServerFixtureCollection)]
    public class BusinessCustomerMoveOutValidationTests
    {
        private readonly BusinessCustomerMoveOutValidationSteps _steps;

        public BusinessCustomerMoveOutValidationTests(TestServerFixture fixture)
        {
            _steps = new BusinessCustomerMoveOutValidationSteps(fixture);
            fixture.Reset();
        }

        [Fact]
        public void StatusHeaderKeyNotPresentReturnsBadRequest()
        {
            this.When(s => _steps.WhenTheRequestExecutes("/business/moveOut", new BusinessCustomerMoveOutValidationModel()))
                .Then(s => _steps.TheResponseCodeIs(HttpStatusCode.Unauthorized))
                .BDDfy();
        }

        [Theory]
        [InlineData(true)]
        public void BusinessCustomerMoveOutValidationIsSuccessful(bool isHealthy)
        {
            CustomerValidationResponse response = null;

            this.Given(s => _steps.AnAuthenticatedUser())
                .And(s => _steps.GivenTheStatusKeyHeaderExists())
                .And(s => _steps.GivenPingReturns(isHealthy))
                .When(s => _steps.WhenTheRequestExecutes(
                    "/business/moveOut",
                    new BusinessCustomerMoveOutValidationBuilder()
                    .ForTransactionType("MoveOut")
                    .RequestWithValidBusinessCustomerValidationData()
                    .Build()))
                .Then(s => _steps.TheResponseCodeIs(HttpStatusCode.Unauthorized))
                .And(s => _steps.TheReturnedContentModelIs(response))
                .BDDfy();
        }

        [Theory]
        [InlineData(true)]
        public void BusinessCustomerValidationFails(bool isHealthy)
        {
            CustomerValidationResponse response = null;

            this.Given(s => _steps.AnAuthenticatedUser())
                .And(s => _steps.GivenTheStatusKeyHeaderExists())
                .And(s => _steps.GivenPingReturns(isHealthy))
                .When(s => _steps.WhenTheRequestExecutes(
                    "/business/moveOut",
                    new BusinessCustomerMoveOutValidationBuilder()
                    .ForTransactionType("MoveOut")
                    .RequestWithInvalidBusinessCustomerValidationData()
                    .Build()))
                .Then(s => _steps.TheResponseCodeIs(HttpStatusCode.Unauthorized))
                .And(s => _steps.TheReturnedContentModelIs(response))
                .BDDfy();
        }
    }
}
