using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Medicare validator
    /// </summary>
    public class MedicareValidator : AbstractValidator<IdentificationDocumentMedicare>
    {
        /// <summary>
        /// Builds validation rules for Medicare validator
        /// </summary>
        public MedicareValidator()
        {
            RuleFor(x => x.MedicareNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.MedicareNumberBlank.Code)
                .WithMessage(ApiErrorMessages.MedicareNumberBlank.Message)
                .Length(10)
                .WithErrorCode(ApiErrorMessages.MedicareNumberBlank.Code)
                .WithMessage(ApiErrorMessages.MedicareNumberBlank.Message)
                .Matches(CustomValidator.NumbersWithoutSpaces)
                .WithErrorCode(ApiErrorMessages.MedicareNumberBlank.Code)
                .WithMessage(ApiErrorMessages.MedicareNumberBlank.Message);

            RuleFor(x => x.IndividualReferenceNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.MedicareIndividualRefNumberBlank.Code)
                .WithMessage(ApiErrorMessages.MedicareIndividualRefNumberBlank.Message)
                .Length(1)
                .WithErrorCode(ApiErrorMessages.MedicareIndividualRefNumberBlank.Code)
                .WithMessage(ApiErrorMessages.MedicareIndividualRefNumberBlank.Message)
                .Matches(CustomValidator.NumbersWithoutSpaces)
                .WithErrorCode(ApiErrorMessages.MedicareIndividualRefNumberBlank.Code)
                .WithMessage(ApiErrorMessages.MedicareIndividualRefNumberBlank.Message);
        }
    }
}
