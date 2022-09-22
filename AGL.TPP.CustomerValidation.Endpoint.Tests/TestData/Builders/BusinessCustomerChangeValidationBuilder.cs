namespace AGL.TPP.CustomerValidation.Endpoint.Tests.TestData.Builders
{
    using AGL.TPP.CustomerValidation.API.Models;
    using AutoFixture;
    using Newtonsoft.Json;

    public class BusinessCustomerChangeValidationBuilder
    {
        private BusinessCustomerChangeValidationModel request;
        private readonly Fixture fixture;

        public BusinessCustomerChangeValidationBuilder()
        {
            request = new BusinessCustomerChangeValidationModel
            {
                Header = new ChangeCustomerValidationHeaderModel(),
                Payload = new BusinessCustomerChangeValidationBodyModel()
            };

            fixture = new Fixture();
        }

        public BusinessCustomerChangeValidationBuilder ForTransactionType(string transactionType)
        {
            request.Header.TransactionType = transactionType;

            return this;
        }

        public BusinessCustomerChangeValidationBuilder RequestWithValidBusinessCustomerChangeValidationData()
        {
            request = JsonConvert.DeserializeObject<BusinessCustomerChangeValidationModel>(System.IO.File.ReadAllText(@"TestData/business-change-customer.json"));

            return this;
        }

        public BusinessCustomerChangeValidationBuilder RequestWithInvalidBusinessCustomerChangeValidationData()
        {
            request = JsonConvert.DeserializeObject<BusinessCustomerChangeValidationModel>(System.IO.File.ReadAllText(@"TestData/business-change-customer-invalid.json"));

            return this;
        }

        public BusinessCustomerChangeValidationModel Build()
        {
            return request;
        }
    }
}
