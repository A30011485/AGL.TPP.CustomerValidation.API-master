using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Drivers license validator
    /// </summary>
    public class DriverLicenseValidator : AbstractValidator<IdentificationDocumentDriverLicense>
    {
        /// <summary>
        /// Builds validation rules for drivers license
        /// </summary>
        public DriverLicenseValidator(CustomerTypes customerType)
        {
            RuleFor(x => x.LicenseNumber)
                .NotEmpty()
                .Length(4, 9)
                .WithErrorCode(customerType == CustomerTypes.RES ? ApiErrorMessages.DrivingLicenseInvalid.Code : ApiErrorMessages.AuthorisedPersonDrivingLicenseInvalid.Code)
                .WithMessage(customerType == CustomerTypes.RES ? ApiErrorMessages.DrivingLicenseInvalid.Message : ApiErrorMessages.AuthorisedPersonDrivingLicenseInvalid.Message)
                .Matches(@"^[A-Za-z0-9]*$")
                .WithErrorCode(customerType == CustomerTypes.RES ? ApiErrorMessages.DrivingLicenseInvalid.Code : ApiErrorMessages.AuthorisedPersonDrivingLicenseInvalid.Code)
                .WithMessage(customerType == CustomerTypes.RES ? ApiErrorMessages.DrivingLicenseInvalid.Message : ApiErrorMessages.AuthorisedPersonDrivingLicenseInvalid.Message)
                .When(x => x.LicenseNumber.Length > 0);
        }
    }
}
