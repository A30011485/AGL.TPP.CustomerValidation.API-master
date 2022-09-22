﻿using AGL.TPP.CustomerValidation.API.Models;
using Swashbuckle.AspNetCore.Examples;

namespace AGL.TPP.CustomerValidation.API.Swagger.Examples
{
    public class BusinessCustomerChangeValidationExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var businessCustomerValidation = new BusinessCustomerChangeValidationModel
            {
                Header = new ChangeCustomerValidationHeaderModel
                {
                    VendorBusinessPartnerNumber = "0123456789",
                    VendorName = "FC",
                    Channel = "TeleMarketer",
                    Retailer = "AGL",
                    TransactionType = "Change",
                    VendorLeadId = "0123456789",
                    ResubmissionCount = "0",
                    ResubmissionComments = ""
                },
                Payload = new BusinessCustomerChangeValidationBodyModel
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
                    MoveDetail = new MoveDetail
                    {
                        MoveIn = new MoveDetailMoveIn
                        {
                            Electricity = new MoveDetailMoveInElectricity
                            {
                                Date = "2019-01-01"
                            },
                            Gas = new MoveDetailMoveInGas
                            {
                                Date = "2019-01-01"
                            }
                        },
                        MoveOut = new MoveDetailMoveOut
                        {
                            Electricity = new MoveDetailMoveOutElectricity
                            {
                                Date = "2019-01-01"
                            },
                            Gas = new MoveDetailMoveOutGas
                            {
                                Date = "2019-01-01"
                            }
                        }
                    },
                    SiteAdditionalDetail = new SiteAdditionalDetailChange
                    {
                         ChangeRequestDate = "2019-01-01",
                         Comments = "Change of mind"
                    }
                }
            };

            return businessCustomerValidation;
        }
    }
}
