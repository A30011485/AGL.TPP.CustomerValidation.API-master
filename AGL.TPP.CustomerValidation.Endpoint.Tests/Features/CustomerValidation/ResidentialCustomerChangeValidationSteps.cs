namespace AGL.TPP.CustomerValidation.Endpoint.Tests.Features.ResidentialCustomerValidation
{
    using AGL.TPP.CustomerValidation.API.Models.Repository;
    using AGL.TPP.CustomerValidation.API.Storage.Services;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.Fixtures;

    public class ResidentialCustomerChangeValidationSteps : TestStepsBase
    {
        private readonly ITableRepository<CustomerValidationData> _salesOrderRepository;

        public ResidentialCustomerChangeValidationSteps(TestServerFixture testServerFixture) : base(testServerFixture)
        {
            _salesOrderRepository = testServerFixture.GetService<ITableRepository<CustomerValidationData>>();
        }
    }
}
