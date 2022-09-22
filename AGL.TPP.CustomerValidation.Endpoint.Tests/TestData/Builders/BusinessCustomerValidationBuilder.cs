namespace AGL.TPP.CustomerValidation.Endpoint.Tests.TestData.Builders
{
    using AGL.TPP.CustomerValidation.API.Models;
    using AutoFixture;
    using Newtonsoft.Json;

    public class BusinessCustomerValidationBuilder
    {
        private BusinessCustomerSalesValidationModel request;
        private readonly Fixture fixture;

        public BusinessCustomerValidationBuilder()
        {
            request = new BusinessCustomerSalesValidationModel
            {
                Header = new CustomerValidationHeaderModel(),
                Payload = new BusinessCustomerSalesValidationBodyModel()
            };

            fixture = new Fixture();
        }

        public BusinessCustomerValidationBuilder ForTransactionType(string transactionType)
        {
            request.Header.TransactionType = transactionType;

            return this;
        }

        public BusinessCustomerValidationBuilder RequestWithValidBusinessCustomerValidationData()
        {
            request = JsonConvert.DeserializeObject<BusinessCustomerSalesValidationModel>(System.IO.File.ReadAllText(@"TestData/business-customer.json"));

            return this;
        }

        public BusinessCustomerValidationBuilder RequestWithInvalidBusinessCustomerValidationData()
        {
            request = JsonConvert.DeserializeObject<BusinessCustomerSalesValidationModel>(System.IO.File.ReadAllText(@"TestData/business-customer-invalid.json"));

            return this;
        }

        public BusinessCustomerSalesValidationModel Build()
        {
            return request;
        }
    }
}
