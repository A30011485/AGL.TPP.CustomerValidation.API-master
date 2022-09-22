namespace AGL.TPP.CustomerValidation.Endpoint.Tests.TestData.Builders
{
    using AGL.TPP.CustomerValidation.API.Models;
    using AutoFixture;
    using Newtonsoft.Json;

    public class BusinessCustomerCancellationBuilder 
    {
        private CancellationBusinessCustomerValidationModel request;
        private readonly Fixture fixture;

        public BusinessCustomerCancellationBuilder ()
        {
            request = new CancellationBusinessCustomerValidationModel
            {
                Header = new CancellationCustomerValidationHeaderModel(),
                Payload = new CancellationBusinessCustomerValidationBodyModel()
            };

            fixture = new Fixture();
        }

        public BusinessCustomerCancellationBuilder  ForTransactionType(string transactionType)
        {
            request.Header.TransactionType = transactionType;

            return this;
        }

        public BusinessCustomerCancellationBuilder  RequestWithValidBusinessCustomerCancellationData()
        {
            request = JsonConvert.DeserializeObject<CancellationBusinessCustomerValidationModel>(System.IO.File.ReadAllText(@"TestData/business-cancel.json"));

            return this;
        }

        public BusinessCustomerCancellationBuilder  RequestWithInvalidBusinessCustomerCancellationData()
        {
            request = JsonConvert.DeserializeObject<CancellationBusinessCustomerValidationModel>(System.IO.File.ReadAllText(@"TestData/business-cancel-invalid.json"));

            return this;
        }

        public CancellationBusinessCustomerValidationModel Build()
        {
            return request;
        }
    }
}
