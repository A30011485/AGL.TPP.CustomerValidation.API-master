using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
  using static Constants;

  /// <summary>
  /// Customer validation header validator
  /// </summary>
  public class CustomerValidationHeaderValidator : AbstractValidator<CustomerValidationHeaderModel>
  {
    /// <summary>
    /// Builds validation rules for customer validation header
    /// </summary>
    public CustomerValidationHeaderValidator()
    {
      RuleFor(x => x.VendorBusinessPartnerNumber)
          .NotEmpty()
          .WithErrorCode(ApiErrorMessages.VendorBusinessPartnerNumberBlank.Code)
          .WithMessage(ApiErrorMessages.VendorBusinessPartnerNumberBlank.Message);

      RuleFor(x => x.VendorName)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotEmpty()
          .WithErrorCode(ApiErrorMessages.VendorNameBlank.Code)
          .WithMessage(ApiErrorMessages.VendorNameBlank.Message)
          .Length(2, 3)
          .WithErrorCode(ApiErrorMessages.VendorNameInvalid.Code)
          .WithMessage(ApiErrorMessages.VendorNameInvalid.Message);

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
          .Must(x => x.ToLowerInvariant().Equals(RetailerAgl) || x.ToLowerInvariant().Equals(RetailerPd))
          .WithErrorCode(ApiErrorMessages.RetailerInvalid.Code)
          .WithMessage(ApiErrorMessages.RetailerInvalid.Message);

      RuleFor(x => x.VendorLeadId)
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

      RuleFor(x => x.DateOfSale)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotEmpty()
          .WithErrorCode(ApiErrorMessages.DateOfSaleBlank.Code)
          .WithMessage(ApiErrorMessages.DateOfSaleBlank.Message)
          .Must(CustomValidator.CheckDateFormat)
          .WithErrorCode(ApiErrorMessages.DateOfSaleInvalid.Code)
          .WithMessage(ApiErrorMessages.DateOfSaleInvalid.Message)
          .Must(CustomValidator.CheckDateNotInFuture)
          .WithErrorCode(ApiErrorMessages.DateOfSaleInFuture.Code)
          .WithMessage(ApiErrorMessages.DateOfSaleInFuture.Message)
          .Must(CustomValidator.CheckDateNotInThePast)
          .WithErrorCode(ApiErrorMessages.DateOfSaleInPast.Code)
          .WithMessage(ApiErrorMessages.DateOfSaleInPast.Message); ;
    }
  }
}
