namespace AGL.TPP.CustomerValidation.Endpoint.Tests.TestData.Builders
{
    using AGL.TPP.CustomerValidation.API.Models;
    using AutoFixture;
    using Newtonsoft.Json;

    public class ResidentialCustomerMoveOutValidationBuilder
    {
        private ResidentialCustomerMoveOutValidationModel request;
        private readonly Fixture fixture;

        public ResidentialCustomerMoveOutValidationBuilder()
        {
            request = new ResidentialCustomerMoveOutValidationModel
            {
                Header = new MoveOutCustomerValidationHeaderModel(),
                Payload = new ResidentialCustomerMoveOutValidationBodyModel()
            };
            fixture = new Fixture();
        }

        public ResidentialCustomerMoveOutValidationBuilder ForTransactionType(string transactionType)
        {
            request.Header.TransactionType = transactionType;

            return this;
        }

        public ResidentialCustomerMoveOutValidationBuilder RequestWithValidResidentialCustomerValidationData()
        {
            request = JsonConvert.DeserializeObject<ResidentialCustomerMoveOutValidationModel>(System.IO.File.ReadAllText(@"TestData/residential-moveout-customer.json"));

            return this;
        }

        public ResidentialCustomerMoveOutValidationBuilder RequestWithInvalidResidentialCustomerValidationData()
        {
            request = JsonConvert.DeserializeObject<ResidentialCustomerMoveOutValidationModel>(System.IO.File.ReadAllText(@"TestData/residential-moveout-customer-invalid.json"));

            return this;
        }

        public ResidentialCustomerMoveOutValidationModel Build()
        {
            return request;
        }
    }
}
