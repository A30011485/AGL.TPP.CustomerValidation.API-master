using AGL.TPP.CustomerValidation.API.Models.Repository;
using AGL.TPP.CustomerValidation.API.Storage.Services;
using AGL.TPP.CustomerValidation.Endpoint.Tests.Fixtures;
using NSubstitute;

namespace AGL.TPP.CustomerValidation.Endpoint.Tests.Features.Status
{
    public class StatusSteps : TestStepsBase
    {
        private readonly ITableRepository<CustomerValidationData> _salesOrderRepository;

        public StatusSteps(TestServerFixture testServerFixture) : base(testServerFixture)
        {
            _salesOrderRepository = testServerFixture.GetService<ITableRepository<CustomerValidationData>>();
        }
    }
}
