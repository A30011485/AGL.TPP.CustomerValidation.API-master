namespace AGL.TPP.CustomerValidation.Endpoint.Tests.TestData.Builders
{
    using AGL.TPP.CustomerValidation.API.Models;
    using AutoFixture;
    using Newtonsoft.Json;

    public class ResidentialCustomerCancellationBuilder
    {
        private CancellationResidentialCustomerValidationModel request;
        private readonly Fixture fixture;

        public ResidentialCustomerCancellationBuilder()
        {
            request = new CancellationResidentialCustomerValidationModel
            {
                Header = new CancellationCustomerValidationHeaderModel(),
                Payload = new CancellationResidentialCustomerValidationBodyModel()
            };
            fixture = new Fixture();
        }

        public ResidentialCustomerCancellationBuilder ForTransactionType(string transactionType)
        {
            request.Header.TransactionType = transactionType;

            return this;
        }

        public ResidentialCustomerCancellationBuilder RequestWithValidResidentialCustomerCancellationData()
        {
            request = JsonConvert.DeserializeObject<CancellationResidentialCustomerValidationModel>(System.IO.File.ReadAllText(@"TestData/residential-cancel.json"));

            return this;
        }

        public ResidentialCustomerCancellationBuilder RequestWithInvalidResidentialCustomerCancellationData()
        {
            request = JsonConvert.DeserializeObject<CancellationResidentialCustomerValidationModel>(System.IO.File.ReadAllText(@"TestData/residential-cancel-invalid.json"));

            return this;
        }

        public CancellationResidentialCustomerValidationModel Build()
        {
            return request;
        }
    }
}
