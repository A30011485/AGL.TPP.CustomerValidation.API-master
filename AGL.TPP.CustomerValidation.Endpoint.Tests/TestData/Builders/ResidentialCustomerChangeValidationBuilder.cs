namespace AGL.TPP.CustomerValidation.Endpoint.Tests.TestData.Builders
{
    using AGL.TPP.CustomerValidation.API.Models;
    using AutoFixture;
    using Newtonsoft.Json;

    public class ResidentialCustomerChangeValidationBuilder
    {
        private ResidentialCustomerChangeValidationModel request;
        private readonly Fixture fixture;

        public ResidentialCustomerChangeValidationBuilder()
        {
            request = new ResidentialCustomerChangeValidationModel
            {
                Header = new ChangeCustomerValidationHeaderModel(),
                Payload = new ResidentialCustomerChangeValidationBodyModel()
            };
            fixture = new Fixture();
        }

        public ResidentialCustomerChangeValidationBuilder ForTransactionType(string transactionType)
        {
            request.Header.TransactionType = transactionType;

            return this;
        }

        public ResidentialCustomerChangeValidationBuilder RequestWithValidResidentialCustomerChangeValidationData()
        {
            request = JsonConvert.DeserializeObject<ResidentialCustomerChangeValidationModel>(System.IO.File.ReadAllText(@"TestData/residential-change-customer.json"));

            return this;
        }

        public ResidentialCustomerChangeValidationBuilder RequestWithInvalidResidentialCustomerChangeValidationData()
        {
            request = JsonConvert.DeserializeObject<ResidentialCustomerChangeValidationModel>(System.IO.File.ReadAllText(@"TestData/residential-change-customer-invalid.json"));

            return this;
        }

        public ResidentialCustomerChangeValidationModel Build()
        {
            return request;
        }
    }
}
