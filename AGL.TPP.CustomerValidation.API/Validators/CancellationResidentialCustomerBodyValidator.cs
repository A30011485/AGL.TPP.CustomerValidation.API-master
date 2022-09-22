using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Residential customer cancellation body validator
    /// </summary>
    public class CancellationResidentialCustomerBodyValidator : AbstractValidator<CancellationResidentialCustomerValidationBodyModel>
    {
        /// <summary>
        /// Build validation rules for Residential customer cancellation body
        /// </summary>
        public CancellationResidentialCustomerBodyValidator()
        {
            RuleFor(x => x.PersonDetail)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.CustomerDetailsBlank.Code)
                .WithMessage(ApiErrorMessages.CustomerDetailsBlank.Message)
                .SetValidator(new CustomerValidator(CustomerTypes.RES));

            #region Contact Detail
            RuleFor(x => x.ContactDetail)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .WithErrorCode(ApiErrorMessages.ContactDetailBlank.Code)
                .WithMessage(ApiErrorMessages.ContactDetailBlank.Message)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.ContactDetailBlank.Code)
                .WithMessage(ApiErrorMessages.ContactDetailBlank.Message)
                .SetValidator(new ContactValidator());

            RuleFor(x => x.ContactDetail)
                    .Must(CustomValidator.AtleastOneContact)
                    .When(x => x.ContactDetail != null)
                    .WithErrorCode(ApiErrorMessages.ContactDetailBlank.Code)
                    .WithMessage(ApiErrorMessages.ContactDetailBlank.Message);

            RuleFor(x => x.ContactDetail.EmailAddress)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.EmailAddressBlank.Code)
                .WithMessage(ApiErrorMessages.EmailAddressBlank.Message)
                .EmailAddress()
                .WithErrorCode(ApiErrorMessages.InvalidEmailAddress.Code)
                .WithMessage(ApiErrorMessages.InvalidEmailAddress.Message)
                .When(x =>
                        x.CustomerConsent?.HasGivenEBillingConsent != null && x.CustomerConsent.HasGivenEBillingConsent.Equals("Y"));

            #endregion

            RuleFor(x => x.SiteAddress)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.SiteAddressBlank.Code)
                .WithMessage(ApiErrorMessages.SiteAddressBlank.Message)
                .SetValidator(new StreetAddressValidator());

            #region Identification
            RuleFor(x => x.Identification)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotNull()
                    .WithErrorCode(ApiErrorMessages.IdentificationDocumentBlank.Code)
                    .WithMessage(ApiErrorMessages.IdentificationDocumentBlank.Message)
                    .Must(CustomValidator.AtleastOneIdentificationDocument)
                    .WithErrorCode(ApiErrorMessages.IdentificationDocumentBlank.Code)
                    .WithMessage(ApiErrorMessages.IdentificationDocumentBlank.Message);

            RuleFor(x => x.Identification.DriversLicense)
                .SetValidator(new DriverLicenseValidator(CustomerTypes.RES))
                .When(x => x.Identification?.DriversLicense?.LicenseNumber?.Length > 0);

            RuleFor(x => x.Identification.Medicare)
                .SetValidator(new MedicareValidator())
                .When(x => x.Identification?.Medicare?.MedicareNumber?.Length > 0);

            RuleFor(x => x.Identification.Passport)
                .SetValidator(new PassportValidator())
                .When(x => x.Identification?.Passport?.PassportNumber?.Length > 0);
            #endregion

            RuleFor(x => x.SiteMeterDetail)
                .SetValidator(new SiteMeterDetailValidator());

            RuleFor(x => x.ConcessionCardDetail.DateOfExpiry)
                .Must(CustomValidator.CheckDateFormat)
                .When(x => x.ConcessionCardDetail?.DateOfExpiry?.Length > 0)
                .WithErrorCode(ApiErrorMessages.ConcessionCardExpiryInvalid.Code)
                .WithMessage(ApiErrorMessages.ConcessionCardExpiryInvalid.Message);

            RuleFor(x => x.CancellationDetail)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.CancellationDetailsBlank.Code)
                .WithMessage(ApiErrorMessages.CancellationDetailsBlank.Message)
                .SetValidator(new CancellationDetailValidator());
        }
    }
}
