using AGL.TPP.CustomerValidation.API.Models;
using Swashbuckle.AspNetCore.Examples;

namespace AGL.TPP.CustomerValidation.API.Swagger.Examples
{
    public class BusinessCustomerCancellationExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var businessCustomerCancellationValidation = new CancellationBusinessCustomerValidationModel
            {
                Header = new CancellationCustomerValidationHeaderModel
                {
                    VendorBusinessPartnerNumber = "0123456789",
                    VendorName = "FC",
                    Channel = "TeleMarketer",
                    Retailer = "AGL",
                    TransactionType = "Cancel",
                    VendorLeadId = "0123456789",
                    ResubmissionCount = "0",
                    ResubmissionComments = ""
                },
                Payload = new CancellationBusinessCustomerValidationBodyModel
                {
                    BusinessDetail = new BusinessDetail
                    {
                        Name = "Bizname"
                    },
                    AuthorisedContactPersonDetail = new Customer
                    {
                        Title = "Mr",
                        FirstName = "John",
                        MiddleName = "A",
                        LastName = "Doe",
                        DateOfBirth = "2019-07-11"
                    },
                    AuthorisedPersonContact = new Contact
                    {
                        EmailAddress = "",
                        MobilePhone = "0400011100",
                        WorkPhone = "0200011100",
                        HomePhone = "0200011100"
                    },
                    SiteAddress = new StreetAddress
                    {
                        BuildingName = "",
                        FloorNumber = "4",
                        LotNumber = "",
                        UnitNumber = "12",
                        StreetNumber = "105",
                        StreetName = "Collins Street",
                        Suburb = "Sampleville",
                        State = "VIC",
                        Postcode = "3000"
                    },
                    MailingAddress = new MailingAddress
                    {
                        StreetAddress = new StreetAddress
                        {
                            BuildingName = "",
                            FloorNumber = "4",
                            LotNumber = "",
                            UnitNumber = "12",
                            StreetNumber = "105",
                            StreetName = "Collins Street",
                            Suburb = "Sampleville",
                            State = "VIC",
                            Postcode = "3000"
                        }
                    },
                    BusinessIdentification = new BusinessIdentification
                    {
                        Abn = "74115061375",
                        Acn = "115061375"
                    },
                    AuthorisedPersonIdentification = new ResidentialIdentification
                    {
                        DriversLicense = new IdentificationDocumentDriverLicense
                        {
                            LicenseNumber = "123456789"
                        }
                    },
                    SiteMeterDetail = new SiteMeterDetail
                    {
                        Electricity = new SiteMeterDetailElectricity
                        {
                            Nmi = "01234567891"
                        },
                        Gas = new SiteMeterDetailGas
                        {
                            Mirn = "01234567891"
                        }
                    },
                    CustomerConsent = new CustomerConsent
                    {
                        HasGivenCreditCheckConsent = "Y",
                        HasGivenEBillingConsent = "N"
                    },
                    CancellationDetail = new CancellationDetail
                    {
                        DateOfCancellation = "2019-02-02",
                        Type = "Electricity",
                        Reason = "Moving out",
                        ReasonOther = ""
                    }
                }
            };

            return businessCustomerCancellationValidation;
        }
    }
}
