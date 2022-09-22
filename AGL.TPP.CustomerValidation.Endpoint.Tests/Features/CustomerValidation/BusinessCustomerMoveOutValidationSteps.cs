namespace AGL.TPP.CustomerValidation.Endpoint.Tests.Features.BusinessCustomerValidation
{
    using AGL.TPP.CustomerValidation.API.Models.Repository;
    using AGL.TPP.CustomerValidation.API.Storage.Services;
    using AGL.TPP.CustomerValidation.Endpoint.Tests.Fixtures;

    public class BusinessCustomerMoveOutValidationSteps : TestStepsBase
    {
        private readonly ITableRepository<CustomerValidationData> _salesOrderRepository;

        public BusinessCustomerMoveOutValidationSteps(TestServerFixture testServerFixture) : base(testServerFixture)
        {
            _salesOrderRepository = testServerFixture.GetService<ITableRepository<CustomerValidationData>>();
        }
    }
}
