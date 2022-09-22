namespace AGL.TPP.CustomerValidation.Endpoint.Tests.Features.BusinessCustomerCancellation
{
    using AGL.TPP.CustomerValidation.API.Models.Repository;
    using AGL.TPP.CustomerValidation.API.Storage.Services;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.Fixtures;

    public class BusinessCustomerCancellationSteps : TestStepsBase
    {
        private readonly ITableRepository<CustomerValidationData> _salesOrderRepository;

        public BusinessCustomerCancellationSteps(TestServerFixture testServerFixture) : base(testServerFixture)
        {
            _salesOrderRepository = testServerFixture.GetService<ITableRepository<CustomerValidationData>>();
        }
    }
}
