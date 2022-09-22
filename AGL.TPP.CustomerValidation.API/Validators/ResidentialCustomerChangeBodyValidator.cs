using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Residential customer body validator
    /// </summary>
    public class ResidentialCustomerChangeBodyValidator : AbstractValidator<ResidentialCustomerChangeValidationBodyModel>
    {
        /// <summary>
        /// Builds validation rules for Residential customer body
        /// </summary>
        public ResidentialCustomerChangeBodyValidator()
        {
            RuleFor(x => x.PersonDetail)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.CustomerDetailsBlank.Code)
                .WithMessage(ApiErrorMessages.CustomerDetailsBlank.Message)
                .SetValidator(new CustomerValidator(CustomerTypes.RES));

            #region Contact Detail
            RuleFor(x => x.ContactDetail)
                .Cascade(CascadeMode.StopOnFirstFailure)
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
                .EmailAddress()
                .WithErrorCode(ApiErrorMessages.InvalidEmailAddress.Code)
                .WithMessage(ApiErrorMessages.InvalidEmailAddress.Message)
                .When(x => x.ContactDetail?.EmailAddress?.Length > 0);

            #endregion

            RuleFor(x => x.SiteAddress)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.SiteAddressBlank.Code)
                .WithMessage(ApiErrorMessages.SiteAddressBlank.Message)
                .SetValidator(new StreetAddressValidator());

            #region Identification
            RuleFor(x => x.Identification)
                        .Cascade(CascadeMode.StopOnFirstFailure)
                        .NotEmpty()
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

            RuleFor(x => x.MoveDetail.MoveIn.Electricity.Date)
                .Must(CustomValidator.CheckDateFormat)
                .When(x => x.MoveDetail?.MoveIn?.Electricity?.Date?.Length > 0)
                .WithErrorCode(ApiErrorMessages.MovedetailMoveinElectricityInvalid.Code)
                .WithMessage(ApiErrorMessages.MovedetailMoveinElectricityInvalid.Message);

            RuleFor(x => x.MoveDetail.MoveIn.Gas.Date)
                .Must(CustomValidator.CheckDateFormat)
                .When(x => x.MoveDetail?.MoveIn?.Gas?.Date?.Length > 0)
                .WithErrorCode(ApiErrorMessages.MovedetailMoveinGasInvalid.Code)
                .WithMessage(ApiErrorMessages.MovedetailMoveinGasInvalid.Message);

            RuleFor(x => x.MoveDetail.MoveOut.Electricity.Date)
                .Must(CustomValidator.CheckDateFormat)
                .When(x => x.MoveDetail?.MoveOut?.Electricity?.Date?.Length > 0)
                .WithErrorCode(ApiErrorMessages.MovedetailMoveoutElectricityInvalid.Code)
                .WithMessage(ApiErrorMessages.MovedetailMoveoutElectricityInvalid.Message);

            RuleFor(x => x.MoveDetail.MoveOut.Gas.Date)
                .Must(CustomValidator.CheckDateFormat)
                .When(x => x.MoveDetail?.MoveOut?.Gas?.Date?.Length > 0)
                .WithErrorCode(ApiErrorMessages.MovedetailMoveoutGasInvalid.Code)
                .WithMessage(ApiErrorMessages.MovedetailMoveoutGasInvalid.Message);

            RuleFor(x => x.SiteMeterDetail)
                .SetValidator(new SiteMeterDetailValidator());

            RuleFor(x => x.SiteAdditionalDetail)
                .SetValidator(new SiteAdditionalDetailChangeValidator());

        }
    }
}