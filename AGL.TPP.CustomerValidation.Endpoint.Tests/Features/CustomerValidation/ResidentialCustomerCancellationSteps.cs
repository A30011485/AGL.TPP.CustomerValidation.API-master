namespace AGL.TPP.CustomerValidation.Endpoint.Tests.Features.ResidentialCustomerCancellation
{
    using AGL.TPP.CustomerValidation.API.Models.Repository;
    using AGL.TPP.CustomerValidation.API.Storage.Services;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.Fixtures;

    public class ResidentialCustomerCancellationSteps : TestStepsBase
    {
        private readonly ITableRepository<CustomerValidationData> _salesOrderRepository;

        public ResidentialCustomerCancellationSteps(TestServerFixture testServerFixture) : base(testServerFixture)
        {
            _salesOrderRepository = testServerFixture.GetService<ITableRepository<CustomerValidationData>>();
        }
    }
}
