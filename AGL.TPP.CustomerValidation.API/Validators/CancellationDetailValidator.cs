using System;
using System.Linq;
using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Cancellation detail validator
    /// </summary>
    public class CancellationDetailValidator : AbstractValidator<CancellationDetail>
    {
        /// <summary>
        /// Builds validation rules for cancellation details
        /// </summary>
        public CancellationDetailValidator()
        {
            RuleFor(x => x.DateOfCancellation)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.DateOfCancellationBlank.Code)
                .WithMessage(ApiErrorMessages.DateOfCancellationBlank.Message)
                .Must(CustomValidator.CheckDateFormat)
                .WithErrorCode(ApiErrorMessages.DateOfCancellationInvalid.Code)
                .WithMessage(ApiErrorMessages.DateOfCancellationInvalid.Message);

            RuleFor(x => x.Type)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.CancellationTypeBlank.Code)
                .WithMessage(ApiErrorMessages.CancellationTypeBlank.Message)
                .Must(x =>
                    x != null &&
                    Enum.GetNames(typeof(ConnectionType))
                    .Any(e => e.ToLowerInvariant().Equals(x.ToLowerInvariant())))
                .WithErrorCode(ApiErrorMessages.CancellationTypeInvalid.Code)
                .WithMessage(ApiErrorMessages.CancellationTypeInvalid.Message);

            RuleFor(x => x.Reason)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.CancellationReasonBlank.Code)
                .WithMessage(ApiErrorMessages.CancellationReasonBlank.Message)
                .Must(CustomValidator.ValidateCancellationReasons)
                .WithErrorCode(ApiErrorMessages.CancellationReasonBlank.Code)
                .WithMessage(ApiErrorMessages.CancellationReasonBlank.Message);

            RuleFor(x => x.ReasonOther)
                .NotEmpty()
                .WithErrorCode(ApiErrorMessages.CancellationReasonOtherBlank.Code)
                .WithMessage(ApiErrorMessages.CancellationReasonOtherBlank.Message)
                .When(x => x?.Reason != null && x.Reason.ToLowerInvariant().Equals("other"));
        }
    }
}
