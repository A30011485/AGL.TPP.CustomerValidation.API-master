namespace AGL.TPP.CustomerValidation.API.Models
{
  using System.Linq;

  /// <summary>
  /// Application constants
  /// </summary>
  public class Constants
  {
    public const string CorrelationKey = "Correlation-Id";
    public const string ApiName = "Api-Name";

    /// <summary>
    /// Error codes
    /// </summary>
    public enum ErrorCode
    {
      NotFound,
      InvalidStatusCheckKey,
      BadRequest
    }

    /// <summary>
    /// List of allowed prefixes for Home and Work phones
    /// </summary>
    public static string[] HomeAndWorkPhonePrefixes = Enumerable
                                                    .Range(1, 9)
                                                    .Where(x => x != 4)
                                                    .Select(x => $"0{x}")
                                                    .ToArray();

    public static string RetailerAgl = "agl";
    public static string RetailerPd = "pd";

    public static string AddressPropertyUseRented = "rented";
    public static string AddressPropertyUseOwnerOccupied = "owneroccupied";

    public static string BackEndValidationError = "BackEndValidationError";

    public static string SapErrorMessagesFilePath = @"App_Data\SapErrorMessages\SapErrorMessages.json";

    public static string ThirdPartyApiSuccess = "ThirdPartyApiSuccess";
    public static string ThirdPartyApiError = "ThirdPartyApiError";

    public static string ThirdPartySapSuccess = "ThirdPartySapSuccess";
    public static string ThirdPartySapError = "ThirdPartySapError";

    public static string ThirdPartyResidentialSale = "ThirdPartyResidentialSale";
    public static string ThirdPartyResidentialSdfi = "ThirdPartyResidentialSdfi";
    public static string ThirdPartyResidentialCancel = "ThirdPartyResidentialCancel";
    public static string ThirdPartyResidentialMoveout = "ThirdPartyResidentialMoveout";
    public static string ThirdPartyResidentialChange = "ThirdPartyResidentialChange";

    public static string ThirdPartyBusinessSale = "ThirdPartyBusinessSale";
    public static string ThirdPartyBusinessSdfi = "ThirdPartyBusinessSdfi";
    public static string ThirdPartyBusinessCancel = "ThirdPartyBusinessCancel";
    public static string ThirdPartyBusinessMoveout = "ThirdPartyBusinessMoveout";
    public static string ThirdPartyBusinessChange = "ThirdPartyBusinessChange";

    /// <summary>
    /// Error messages
    /// </summary>
    public class ErrorMessages
    {
      public const string StatusClientKeyHeaderInvalid = "Status-Client-Key header is invalid.";
    }

    /// <summary>
    /// List of all the error messages for API Validation
    /// </summary>
    public class ApiErrorMessages
    {
      public static readonly ApiError UnknownError = new ApiError { Code = "5000", Message = "An internal error has occurred. Please re-try submission." };
      public static readonly ApiError HeaderBlank = new ApiError { Code = "2000", Message = "Please provide a value for Header" };
      public static readonly ApiError PayloadBlank = new ApiError { Code = "2001", Message = "Please provide a value for Payload" };
      public static readonly ApiError VendorNameBlank = new ApiError { Code = "2002", Message = "Please provide a value for Vendor Name" };
      public static readonly ApiError VendorBusinessPartnerNumberBlank = new ApiError { Code = "2003", Message = "Please provide a value for Vendor Business Partner Number" };
      public static readonly ApiError VendorNameInvalid = new ApiError { Code = "2004", Message = "Please provide a valid value for Vendor Name" };
      public static readonly ApiError VendorBusinessPartnerNumberInvalid = new ApiError { Code = "2005", Message = "Please provide a valid value for Vendor Business Partner Number" };
      public static readonly ApiError ChannelBlank = new ApiError { Code = "2006", Message = "Please select one of the following values for Channel - TeleMarketer, OnField, Web, Intervention, Comparator, Broker, Association, ThirdPartyAggregator, Reactivation, NewEnergy." };
      public static readonly ApiError ChannelInvalid = new ApiError { Code = "2007", Message = "Please select one of the following values for Channel - TeleMarketer, OnField, Web, Intervention, Comparator, Broker, Association, ThirdPartyAggregator, Reactivation, NewEnergy." };
      public static readonly ApiError TransactionTypeBlank = new ApiError { Code = "2008", Message = "Please select one of the following values for Transaction Type - Sale, Cancel, Change, SDFI, Moveout." };
      public static readonly ApiError TransactionTypeInvalid = new ApiError { Code = "2009", Message = "Please select one of the following values for Transaction Type - Sale, Cancel, Change, SDFI, Moveout." };
      public static readonly ApiError RetailerBlank = new ApiError { Code = "2010", Message = "Please select AGL or PD (Powerdirect)." };
      public static readonly ApiError RetailerInvalid = new ApiError { Code = "2011", Message = "Please select AGL or PD (Powerdirect)." };
      public static readonly ApiError RetailerLength = new ApiError { Code = "2012", Message = "Character length exceeded. 'Retailer' must be 10 characters or fewer." };
      public static readonly ApiError VendorLeadIdBlank = new ApiError { Code = "2013", Message = "Please provide a value for Vendor Lead ID." };
      public static readonly ApiError VendorLeadIdInvalid = new ApiError { Code = "2014", Message = "Please provide a valid Vendor Lead ID." };
      public static readonly ApiError OfferTypeInvalid = new ApiError { Code = "2015", Message = "Please provide one of the following values for Offer Type - AcquisitionDualFuel, AcquisitionElectricity, AcquisitionGas, CrossSellElectricity, CrossSellGas, RetentionDualFuel, RetentionElectricity, RetentionGas." };
      public static readonly ApiError DateOfSaleBlank = new ApiError { Code = "2016", Message = "Please provide a Date of Sale (YYYY-MM-DD)." };
      public static readonly ApiError DateOfSaleInvalid = new ApiError { Code = "2017", Message = "Please provide a Date of Sale (YYYY-MM-DD)." };
      public static readonly ApiError DateOfSaleInFuture = new ApiError { Code = "2600", Message = "Sale Date cannot be in the future." };
      public static readonly ApiError DateOfSaleInPast = new ApiError { Code = "2601", Message = "Please provide valid date of sale - cannot be more than 30 days in the past." };

      public static readonly ApiError AuthorisedPersonContactBlank = new ApiError { Code = "2018", Message = "Please provide a value for Authorised Contact Person." };
      public static readonly ApiError SiteAddressBlank = new ApiError { Code = "2019", Message = "Please provide a value for Site Address." };
      public static readonly ApiError CustomerDetailsBlank = new ApiError { Code = "2020", Message = "Please provide value for Customer Details." };
      public static readonly ApiError CancellationDetailsBlank = new ApiError { Code = "2021", Message = "Please provide value for Cancellation Details." };

      public static readonly ApiError TitleBlank = new ApiError { Code = "2022", Message = "Please select one of the following values for Title - Mr, Mrs, Ms, Miss, Dr,Mx." };
      public static readonly ApiError TitleInvalid = new ApiError { Code = "2023", Message = "Please select one of the following values for Title - Mr, Mrs, Ms, Miss, Dr,Mx." };
      public static readonly ApiError FirstNameBlank = new ApiError { Code = "2024", Message = "Please provide a First Name." };
      public static readonly ApiError FirstNameInvalid = new ApiError { Code = "2025", Message = "First Name accepts only alpha characters." };
      public static readonly ApiError FirstNameLength = new ApiError { Code = "2026", Message = "Character length exceeded. 'First Name' must be between 1 and 60 characters." };
      public static readonly ApiError MiddleNameLength = new ApiError { Code = "2027", Message = "Character length exceeded. 'Middle Name' must be between 1 and 60 characters." };
      public static readonly ApiError MiddleNameInvalid = new ApiError { Code = "2028", Message = "Middle Name accepts only alpha characters." };
      public static readonly ApiError LastNameBlank = new ApiError { Code = "2029", Message = "Please provide a Last Name." };
      public static readonly ApiError LastNameInvalid = new ApiError { Code = "2030", Message = "Last Name accepts only alpha characters." };
      public static readonly ApiError LastNameLength = new ApiError { Code = "2031", Message = "Character length exceeded. 'Last Name' must be between 1 and 60 characters." };
      public static readonly ApiError DateOfBirthBlank = new ApiError { Code = "2032", Message = "Please provide a Date of Birth (YYYY-MM-DD)." };
      public static readonly ApiError DateOfBirthInvalid = new ApiError { Code = "2033", Message = "Please provide a Date of Birth (YYYY-MM-DD). You must be over 18 years of age." };
      public static readonly ApiError DateOfBirthAge = new ApiError { Code = "2034", Message = "Please provide a Date of Birth (YYYY-MM-DD). You must be over 18 years of age." };

      public static readonly ApiError AuthorisedPersonTitleBlank = new ApiError { Code = "2035", Message = "Please select one of the following values for Title - Mr, Mrs, Ms, Miss, Dr,Mx" };
      public static readonly ApiError AuthorisedPersonTitleInvalid = new ApiError { Code = "2036", Message = "Please select one of the following values for Title - Mr, Mrs, Ms, Miss, Dr,Mx" };
      public static readonly ApiError AuthorisedPersonFirstNameBlank = new ApiError { Code = "2037", Message = "Please provide Authorised Person's First Name." };
      public static readonly ApiError AuthorisedPersonFirstNameInvalid = new ApiError { Code = "2038", Message = "Authorised Person's First Name must be alpha characters." };
      public static readonly ApiError AuthorisedPersonFirstNameLength = new ApiError { Code = "2039", Message = "Character length exceeded. 'Authorised Person's First Name' must be between 1 and 60 characters." };
      public static readonly ApiError AuthorisedPersonMiddleNameLength = new ApiError { Code = "2040", Message = "Character length exceeded. 'Authorised Person's Middle Name' must be between 1 and 60 characters." };
      public static readonly ApiError AuthorisedPersonMiddleNameInvalid = new ApiError { Code = "2041", Message = "Authorised Person's Middle Name must be alpha characters." };
      public static readonly ApiError AuthorisedPersonLastNameBlank = new ApiError { Code = "2042", Message = "Please provide Authorised Person's Last Name." };
      public static readonly ApiError AuthorisedPersonLastNameInvalid = new ApiError { Code = "2043", Message = "Authorised Person's Last Name must be alpha characters." };
      public static readonly ApiError AuthorisedPersonLastNameLength = new ApiError { Code = "2044", Message = "Character length exceeded. 'Authorised Person's Last Name' must be between 1 and 60 characters." };
      public static readonly ApiError AuthorisedPersonDateOfBirthBlank = new ApiError { Code = "2045", Message = "Please provide Authorised Person's Date of Birth (YYYY-MM-DD)." };
      public static readonly ApiError AuthorisedPersonDateOfBirthInvalid = new ApiError { Code = "2046", Message = "Please provide Authorised Person's Date of Birth (YYYY-MM-DD). You must be over 18 years of age." };
      public static readonly ApiError AuthorisedPersonDateOfBirthAge = new ApiError { Code = "2047", Message = "Please provide Authorised Person's Date of Birth (YYYY-MM-DD). You must be over 18 years of age." };

      public static readonly ApiError ContactDetailBlank = new ApiError { Code = "2048", Message = "Please provide at least one contact number." };
      public static readonly ApiError MobilePhoneInvalid = new ApiError { Code = "2049", Message = "Please provide a valid Mobile phone number." };
      public static readonly ApiError MobilePhoneLength = new ApiError { Code = "2050", Message = "Mobile phone number needs to have a length of 10 or 11 digits." };
      public static readonly ApiError HomePhoneInvalid = new ApiError { Code = "2051", Message = "Please provide valid Home phone number." };
      public static readonly ApiError WorkPhoneInvalid = new ApiError { Code = "2052", Message = "Please provide valid Work phone number." };
      public static readonly ApiError EmailAddressBlank = new ApiError { Code = "2053", Message = "Please provide a current Email address." };
      public static readonly ApiError InvalidEmailAddress = new ApiError { Code = "2602", Message = "Invalid email received '{PropertyValue}'. Please provide valid email address." };

      public static readonly ApiError StreetNameBlank = new ApiError { Code = "2054", Message = "Please provide valid Street Name." };
      public static readonly ApiError StreetNumberBlank = new ApiError { Code = "2055", Message = "Please provide valid Street Number." };
      public static readonly ApiError SuburbInvalid = new ApiError { Code = "2056", Message = "Please provide valid Suburb." };
      public static readonly ApiError StateInvalid = new ApiError { Code = "2057", Message = "Please provide valid State." };
      public static readonly ApiError PostcodeBlank = new ApiError { Code = "2058", Message = "Please provide valid Postcode." };
      public static readonly ApiError MailingAddressBlank = new ApiError { Code = "2059", Message = "Please provide valid Mailing Address." };

      public static readonly ApiError IdentificationDocumentBlank = new ApiError { Code = "2060", Message = "Please provide your identification type. i.e. Driver Licence or Medicare or Passport." };
      public static readonly ApiError BusinessIdentificationDocumentBlank = new ApiError { Code = "2061", Message = "Please provide your identification type i.e. ABN or ACN." };
      public static readonly ApiError DrivingLicenseInvalid = new ApiError { Code = "2062", Message = "Please provide valid Driver's License number. License Number must be between 4 and 9 characters." };
      public static readonly ApiError AuthorisedPersonDrivingLicenseInvalid = new ApiError { Code = "2063", Message = "Please provide valid Authorised Person's Drivers License number. License Number must be between 4 and 9 characters." };
      public static readonly ApiError MedicareNumberBlank = new ApiError { Code = "2064", Message = "Please provide valid Medicare number including your individual reference number, or alternate form of identification i.e. Driver Licence or Passport." };
      public static readonly ApiError MedicareIndividualRefNumberBlank = new ApiError { Code = "2065", Message = "Please provide valid Medicare individual reference number." };
      public static readonly ApiError PassportNumberBlank = new ApiError { Code = "2066", Message = "Please provide valid Passport number or alternate form of identification i.e. Drivers Licence or Medicare." };

      public static readonly ApiError ConcessionCardDetailsBlank = new ApiError { Code = "2067", Message = "Please provide value for Concession Card Details." };
      public static readonly ApiError ConcessionCardNumberBlank = new ApiError { Code = "2068", Message = "Please provide a value for Concession Card number." };
      public static readonly ApiError ConcessionCardTypeInvalid = new ApiError { Code = "2069", Message = "Please provide valid Concession Card type." };
      public static readonly ApiError ConcessionCardExpiryInvalid = new ApiError { Code = "2070", Message = "Please provide valid Date of Expiry for the Concession Card." };

      public static readonly ApiError EbillingInvalid = new ApiError { Code = "2071", Message = "Do you consent to E-billing? Please select Y or N." };
      public static readonly ApiError CreditCheckBlank = new ApiError { Code = "2072", Message = "Do you consent to a credit check? Please select Y or N." };
      public static readonly ApiError CreditCheckInvalid = new ApiError { Code = "2073", Message = "Do you consent to a credit check? Please select Y or N." };
      public static readonly ApiError DateOfCancellationBlank = new ApiError { Code = "2074", Message = "Please advise the preferred Date of Cancellation." };
      public static readonly ApiError DateOfCancellationInvalid = new ApiError { Code = "2075", Message = "Please provide a valid Date of Cancellation." };

      public static readonly ApiError CancellationTypeBlank = new ApiError { Code = "2076", Message = "Please provide valid Cancellation Type - ELECTRICITY, GAS, DUAL." };
      public static readonly ApiError CancellationTypeInvalid = new ApiError { Code = "2077", Message = "Please provide valid Cancellation Type - ELECTRICITY, GAS, DUAL." };
      public static readonly ApiError CancellationReasonBlank = new ApiError { Code = "2078", Message = "Please select a valid Cancellation Reason." };
      public static readonly ApiError CancellationReasonOtherBlank = new ApiError { Code = "2079", Message = "Please provide comments for Cancellation Reason 'Other'." };

      public static readonly ApiError BusinessNameBlank = new ApiError { Code = "2080", Message = "Please provide a Business Name." };
      public static readonly ApiError BusinessNameInvalid = new ApiError { Code = "2081", Message = "Please provide valid Business Name." };
      public static readonly ApiError AbnInvalid = new ApiError { Code = "2082", Message = "Please provide valid ABN." };
      public static readonly ApiError AcnInvalid = new ApiError { Code = "2083", Message = "Please provide valid ACN." };

      public static readonly ApiError NmiInvalid = new ApiError { Code = "2084", Message = "Please provide valid NMI." };
      public static readonly ApiError MirnInvalid = new ApiError { Code = "2085", Message = "Please provide valid MIRN." };

      public static readonly ApiError ChangeRequestDateInvalid = new ApiError { Code = "2086", Message = "Please provide a valid Date for Change Request (YYYY-MM-DD)." };
      public static readonly ApiError ChangeRequestDateBlank = new ApiError { Code = "2087", Message = "Please provide a valid Date for Change Request (YYYY-MM-DD)." };
      public static readonly ApiError CommentsBlank = new ApiError { Code = "2088", Message = "Please provide Comments for Change in Order." };
      public static readonly ApiError AddressPropertyUseInvalid = new ApiError { Code = "2089", Message = "Please provide valid value for Address Property Use." };

      public static readonly ApiError MovedetailMoveinElectricityInvalid = new ApiError { Code = "2090", Message = "Please provide valid Move in Electricity Date (YYYY-MM-DD)." };
      public static readonly ApiError MovedetailMoveinGasInvalid = new ApiError { Code = "2091", Message = "Please provide valid Move in Gas Date (YYYY-MM-DD)." };
      public static readonly ApiError MovedetailMoveoutElectricityInvalid = new ApiError { Code = "2092", Message = "Please provide valid Move out Electricity Date (YYYY-MM-DD)." };
      public static readonly ApiError MovedetailMoveoutGasInvalid = new ApiError { Code = "2093", Message = "Please provide valid Move out Gas Date (YYYY-MM-DD)." };

      public static readonly ApiError ResubmissionCountInvalid = new ApiError { Code = "2555", Message = "Please provide a valid value for resubmission count." };
      public static readonly ApiError ResubmissionCommentInvalid = new ApiError { Code = "2556", Message = "Please provide a valid value for resubmission comments" };

      public static readonly ApiError DomainValidationInvalidDomain = new ApiError { Code = "2603", Message = "Invalid email domain received '{0}'. Please provide valid email domain and address" };
      public static readonly ApiError DomainValidationEmptyDomain = new ApiError { Code = "2604", Message = "Email address must not be blank. Please provide valid email address" };

    }

    public class ApiError
    {
      public string Code { get; set; }

      public string Message { get; set; }
    }
  }
}
