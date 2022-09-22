using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
  using static Constants;

  /// <summary>
  /// Customer cancellation header validator
  /// </summary>
  public class CancellationCustomerValidationHeaderValidator : AbstractValidator<CancellationCustomerValidationHeaderModel>
  {
    /// <summary>
    /// Builds validation rules for customer cancellation
    /// </summary>
    public CancellationCustomerValidationHeaderValidator()
    {
      RuleFor(x => x.VendorBusinessPartnerNumber).NotEmpty();
      RuleFor(x => x.VendorName).NotEmpty();

      RuleFor(x => x.Channel)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotEmpty()
          .WithErrorCode(ApiErrorMessages.ChannelBlank.Code)
          .WithMessage(ApiErrorMessages.ChannelBlank.Message)
          .Must(CustomValidator.ValidateChannel)
          .WithErrorCode(ApiErrorMessages.ChannelInvalid.Code)
          .WithMessage(ApiErrorMessages.ChannelInvalid.Message);

      RuleFor(x => x.TransactionType)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotEmpty()
          .WithErrorCode(ApiErrorMessages.TransactionTypeBlank.Code)
          .WithMessage(ApiErrorMessages.TransactionTypeBlank.Message)
          .Must(CustomValidator.ValidateTransactionTypes)
          .WithErrorCode(ApiErrorMessages.TransactionTypeInvalid.Code)
          .WithMessage(ApiErrorMessages.TransactionTypeInvalid.Message);

      RuleFor(x => x.Retailer)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotEmpty()
          .WithErrorCode(ApiErrorMessages.RetailerBlank.Code)
          .WithMessage(ApiErrorMessages.RetailerBlank.Message)
          .MaximumLength(10)
          .WithErrorCode(ApiErrorMessages.RetailerLength.Code)
          .WithMessage(ApiErrorMessages.RetailerLength.Message)
          .Must(x => x.ToLowerInvariant().Equals("agl") || x.ToLowerInvariant().Equals("pd"))
          .WithErrorCode(ApiErrorMessages.RetailerInvalid.Code)
          .WithMessage(ApiErrorMessages.RetailerInvalid.Message);

      RuleFor(x => x.VendorLeadId)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotEmpty()
          .WithErrorCode(ApiErrorMessages.VendorLeadIdBlank.Code)
          .WithMessage(ApiErrorMessages.VendorLeadIdBlank.Message);

      RuleFor(x => x.ResubmissionCount)
          .Matches(CustomValidator.NumbersWithoutSpaces)
          .WithMessage(ApiErrorMessages.ResubmissionCountInvalid.Message)
          .When(x => !string.IsNullOrEmpty(x.ResubmissionCount));

      RuleFor(x => x.ResubmissionComments)
          .NotEmpty()
          .WithMessage(ApiErrorMessages.ResubmissionCommentInvalid.Message)
          .When(x => !string.IsNullOrEmpty(x.ResubmissionCount));

      RuleFor(x => x.OfferType)
        .Cascade(CascadeMode.StopOnFirstFailure)
        .NotEmpty()
        .WithErrorCode(ApiErrorMessages.OfferTypeInvalid.Code)
        .WithMessage(ApiErrorMessages.OfferTypeInvalid.Message)
        .Must(CustomValidator.ValidateOfferTypes)
        .WithErrorCode(ApiErrorMessages.OfferTypeInvalid.Code)
        .WithMessage(ApiErrorMessages.OfferTypeInvalid.Message);
    }
  }
}
