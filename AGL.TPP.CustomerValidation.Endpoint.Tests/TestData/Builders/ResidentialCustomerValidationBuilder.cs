namespace AGL.TPP.CustomerValidation.Endpoint.Tests.TestData.Builders
{
    using AGL.TPP.CustomerValidation.API.Models;
    using AutoFixture;
    using Newtonsoft.Json;

    public class ResidentialCustomerValidationBuilder
    {
        private ResidentialCustomerSalesValidationModel request;
        private readonly Fixture fixture;

        public ResidentialCustomerValidationBuilder()
        {
            request = new ResidentialCustomerSalesValidationModel
            {
                Header = new CustomerValidationHeaderModel(),
                Payload = new ResidentialCustomerSalesValidationBodyModel()
            };
            fixture = new Fixture();
        }

        public ResidentialCustomerValidationBuilder ForTransactionType(string transactionType)
        {
            request.Header.TransactionType = transactionType;

            return this;
        }

        public ResidentialCustomerValidationBuilder RequestWithValidResidentialCustomerValidationData()
        {
            request = JsonConvert.DeserializeObject<ResidentialCustomerSalesValidationModel>(System.IO.File.ReadAllText(@"TestData/residential-customer.json"));

            return this;
        }

        public ResidentialCustomerValidationBuilder RequestWithInvalidResidentialCustomerValidationData()
        {
            request = JsonConvert.DeserializeObject<ResidentialCustomerSalesValidationModel>(System.IO.File.ReadAllText(@"TestData/residential-customer-invalid.json"));

            return this;
        }

        public ResidentialCustomerSalesValidationModel Build()
        {
            return request;
        }
    }
}
