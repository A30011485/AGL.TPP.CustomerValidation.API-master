using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Business customer cancellation body validator
    /// </summary>
    public class CancellationBusinessCustomerBodyValidator : AbstractValidator<CancellationBusinessCustomerValidationBodyModel>
    {
        /// <summary>
        /// Builds validation rules for business customer cancellation body
        /// </summary>
        public CancellationBusinessCustomerBodyValidator()
        {
            RuleFor(x => x.BusinessDetail)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.BusinessNameBlank.Code)
                .WithMessage(ApiErrorMessages.BusinessNameBlank.Message)
                .SetValidator(new BusinessDetailValidator());

            RuleFor(x => x.AuthorisedContactPersonDetail)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.AuthorisedPersonContactBlank.Code)
                .WithMessage(ApiErrorMessages.AuthorisedPersonContactBlank.Message)
                .SetValidator(new CustomerValidator(CustomerTypes.BUS));

            #region Contact Detail
            RuleFor(x => x.AuthorisedPersonContact)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.ContactDetailBlank.Code)
                .WithMessage(ApiErrorMessages.ContactDetailBlank.Message)
                .SetValidator(new ContactValidator());

            RuleFor(x => x.AuthorisedPersonContact)
                    .Must(CustomValidator.AtleastOneContact)
                    .When(x => x.AuthorisedPersonContact != null)
                    .WithErrorCode(ApiErrorMessages.ContactDetailBlank.Code)
                    .WithMessage(ApiErrorMessages.ContactDetailBlank.Message);

            #endregion

            RuleFor(x => x.SiteAddress)
               .NotEmpty()
               .WithErrorCode(ApiErrorMessages.SiteAddressBlank.Code)
               .WithMessage(ApiErrorMessages.SiteAddressBlank.Message)
               .SetValidator(new StreetAddressValidator());

            #region Identification
            RuleFor(x => x.BusinessIdentification)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty()
                    .WithErrorCode(ApiErrorMessages.BusinessIdentificationDocumentBlank.Code)
                    .WithMessage(ApiErrorMessages.BusinessIdentificationDocumentBlank.Message)
                    .Must(x => x.Abn?.Length > 0 || x.Acn?.Length > 0)
                    .WithErrorCode(ApiErrorMessages.BusinessIdentificationDocumentBlank.Code)
                    .WithMessage(ApiErrorMessages.BusinessIdentificationDocumentBlank.Message);

            RuleFor(x => x.BusinessIdentification)
               .Must(x => x.Abn?.Length == 11)
               .When(x => x.BusinessIdentification?.Abn?.Length > 0)
               .WithErrorCode(ApiErrorMessages.AbnInvalid.Code)
               .WithMessage(ApiErrorMessages.AbnInvalid.Message);

            RuleFor(x => x.BusinessIdentification)
               .Must(x => x.Acn?.Length == 9)
               .When(x => x.BusinessIdentification?.Acn?.Length > 0)
               .WithErrorCode(ApiErrorMessages.AcnInvalid.Code)
               .WithMessage(ApiErrorMessages.AcnInvalid.Message);

            RuleFor(x => x.AuthorisedPersonIdentification.DriversLicense)
               .SetValidator(new DriverLicenseValidator(CustomerTypes.BUS))
               .When(x => x.AuthorisedPersonIdentification?.DriversLicense?.LicenseNumber?.Length > 0);
            #endregion

            RuleFor(x => x.SiteMeterDetail)
                .SetValidator(new SiteMeterDetailValidator());

            RuleFor(x => x.CancellationDetail)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.CancellationDetailsBlank.Code)
                .WithMessage(ApiErrorMessages.CancellationDetailsBlank.Message)
                .SetValidator(new CancellationDetailValidator());
        }
    }
}
