namespace AGL.TPP.CustomerValidation.Endpoint.Tests.Features.BusinessCustomerCancellation
{
    using System.Net;
    using AGL.TPP.CustomerValidation.API.Models;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.Fixtures;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.TestData.Builders;
    using TestStack.BDDfy;
    using Xunit;

    [Collection(FixtureCollections.TestServerFixtureCollection)]
    public class BusinessCustomerCancellationTests
    {
        private readonly BusinessCustomerCancellationSteps _steps;

        public BusinessCustomerCancellationTests(TestServerFixture fixture)
        {
            _steps = new BusinessCustomerCancellationSteps(fixture);
            fixture.Reset();
        }

        [Fact]
        public void StatusHeaderKeyNotPresentReturnsBadRequest()
        {
            this.When(s => _steps.WhenTheRequestExecutes("/business/cancellation", new BusinessCustomerSalesValidationModel()))
                .Then(s => _steps.TheResponseCodeIs(HttpStatusCode.Unauthorized))
                .BDDfy();
        }

        [Theory]
        [InlineData(true)]
        public void BusinessCustomerCancellationValidationIsSuccessful(bool isHealthy)
        {
            CustomerValidationResponse response = null;

            this.Given(s => _steps.AnAuthenticatedUser())
                .And(s => _steps.GivenTheStatusKeyHeaderExists())
                .And(s => _steps.GivenPingReturns(isHealthy))
                .When(s => _steps.WhenTheRequestExecutes("/business/cancellation", 
                    new BusinessCustomerCancellationBuilder()
                    .ForTransactionType("Cancel")
                    .RequestWithValidBusinessCustomerCancellationData()
                    .Build()))
                .Then(s => _steps.TheResponseCodeIs(HttpStatusCode.Unauthorized))
                .And(s => _steps.TheReturnedContentModelIs(response))
                .BDDfy();
        }

        [Theory]
        [InlineData(true)]
        public void BusinessCustomerCancellationValidationFails(bool isHealthy)
        {
            CustomerValidationResponse response = null;

            this.Given(s => _steps.AnAuthenticatedUser())
                .And(s => _steps.GivenTheStatusKeyHeaderExists())
                .And(s => _steps.GivenPingReturns(isHealthy))
                .When(s => _steps.WhenTheRequestExecutes("/business/cancellation",
                    new BusinessCustomerCancellationBuilder()
                    .ForTransactionType("Cancel")
                    .RequestWithInvalidBusinessCustomerCancellationData()
                    .Build()))
                .Then(s => _steps.TheResponseCodeIs(HttpStatusCode.Unauthorized))
                .And(s => _steps.TheReturnedContentModelIs(response))
                .BDDfy();
        }
    }
}
