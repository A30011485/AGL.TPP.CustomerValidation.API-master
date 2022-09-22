namespace AGL.TPP.CustomerValidation.Endpoint.Tests.TestData.Builders
{
    using AGL.TPP.CustomerValidation.API.Models;
    using AutoFixture;
    using Newtonsoft.Json;

    public class BusinessCustomerMoveOutValidationBuilder
    {
        private BusinessCustomerMoveOutValidationModel request;
        private readonly Fixture fixture;

        public BusinessCustomerMoveOutValidationBuilder()
        {
            request = new BusinessCustomerMoveOutValidationModel
            {
                Header = new MoveOutCustomerValidationHeaderModel(),
                Payload = new BusinessCustomerMoveOutValidationBodyModel()
            };

            fixture = new Fixture();
        }

        public BusinessCustomerMoveOutValidationBuilder ForTransactionType(string transactionType)
        {
            request.Header.TransactionType = transactionType;

            return this;
        }

        public BusinessCustomerMoveOutValidationBuilder RequestWithValidBusinessCustomerValidationData()
        {
            request = JsonConvert.DeserializeObject<BusinessCustomerMoveOutValidationModel>(System.IO.File.ReadAllText(@"TestData/business-moveout-customer.json"));

            return this;
        }

        public BusinessCustomerMoveOutValidationBuilder RequestWithInvalidBusinessCustomerValidationData()
        {
            request = JsonConvert.DeserializeObject<BusinessCustomerMoveOutValidationModel>(System.IO.File.ReadAllText(@"TestData/business-moveout-customer-invalid.json"));

            return this;
        }

        public BusinessCustomerMoveOutValidationModel Build()
        {
            return request;
        }
    }
}
