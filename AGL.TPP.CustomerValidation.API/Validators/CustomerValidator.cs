using System;
using System.Linq;
using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation;

namespace AGL.TPP.CustomerValidation.API.Validators
{
    using static Constants;

    /// <summary>
    /// Customer validator
    /// </summary>
    public class CustomerValidator : AbstractValidator<Customer>
    {
        /// <summary>
        /// Builds validation rules for customer
        /// </summary>
        public CustomerValidator(CustomerTypes customerType)
        {
            RuleFor(x => x.Title)

                .Must(MatchTitleEnum)
                .WithErrorCode(customerType == CustomerTypes.RES ? ApiErrorMessages.TitleInvalid.Code : ApiErrorMessages.AuthorisedPersonTitleInvalid.Code)
                .WithMessage(customerType == CustomerTypes.RES ? ApiErrorMessages.TitleInvalid.Message : ApiErrorMessages.AuthorisedPersonTitleInvalid.Message);

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(customerType == CustomerTypes.RES ? ApiErrorMessages.FirstNameBlank.Code : ApiErrorMessages.AuthorisedPersonFirstNameBlank.Code)
                .WithMessage(customerType == CustomerTypes.RES ? ApiErrorMessages.FirstNameBlank.Message : ApiErrorMessages.AuthorisedPersonFirstNameBlank.Message)
                .Length(1, 60)
                .WithErrorCode(customerType == CustomerTypes.RES ? ApiErrorMessages.FirstNameLength.Code : ApiErrorMessages.AuthorisedPersonFirstNameLength.Code)
                .WithMessage(customerType == CustomerTypes.RES ? ApiErrorMessages.FirstNameLength.Message : ApiErrorMessages.AuthorisedPersonFirstNameLength.Message);

            RuleFor(x => x.MiddleName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(60)
                .WithErrorCode(customerType == CustomerTypes.RES ? ApiErrorMessages.MiddleNameLength.Code : ApiErrorMessages.AuthorisedPersonMiddleNameLength.Code)
                .WithMessage(customerType == CustomerTypes.RES ? ApiErrorMessages.MiddleNameLength.Message : ApiErrorMessages.AuthorisedPersonMiddleNameLength.Message)
                .When(x => x.MiddleName?.Length > 0);

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(customerType == CustomerTypes.RES ? ApiErrorMessages.LastNameBlank.Code : ApiErrorMessages.AuthorisedPersonLastNameBlank.Code)
                .WithMessage(customerType == CustomerTypes.RES ? ApiErrorMessages.LastNameBlank.Message : ApiErrorMessages.AuthorisedPersonLastNameBlank.Message)
                .Length(1, 60)
                .WithErrorCode(customerType == CustomerTypes.RES ? ApiErrorMessages.LastNameLength.Code : ApiErrorMessages.AuthorisedPersonLastNameLength.Code)
                .WithMessage(customerType == CustomerTypes.RES ? ApiErrorMessages.LastNameLength.Message : ApiErrorMessages.AuthorisedPersonLastNameLength.Message);

            RuleFor(x => x.DateOfBirth)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode(customerType == CustomerTypes.RES ? ApiErrorMessages.DateOfBirthBlank.Code : ApiErrorMessages.AuthorisedPersonDateOfBirthBlank.Code)
                .WithMessage(customerType == CustomerTypes.RES ? ApiErrorMessages.DateOfBirthBlank.Message : ApiErrorMessages.AuthorisedPersonDateOfBirthBlank.Message)
                .Must(CustomValidator.CheckDateFormat)
                .WithErrorCode(customerType == CustomerTypes.RES ? ApiErrorMessages.DateOfBirthInvalid.Code : ApiErrorMessages.AuthorisedPersonDateOfBirthInvalid.Code)
                .WithMessage(customerType == CustomerTypes.RES ? ApiErrorMessages.DateOfBirthInvalid.Message : ApiErrorMessages.AuthorisedPersonDateOfBirthInvalid.Message)
                .Must(CustomValidator.ValidateAge)
                .WithErrorCode(customerType == CustomerTypes.RES ? ApiErrorMessages.DateOfBirthInvalid.Code : ApiErrorMessages.AuthorisedPersonDateOfBirthInvalid.Code)
                .WithMessage(customerType == CustomerTypes.RES ? ApiErrorMessages.DateOfBirthInvalid.Message : ApiErrorMessages.AuthorisedPersonDateOfBirthInvalid.Message);
        }

        private bool MatchTitleEnum(string x)
        {
            return x != null && Enum.GetNames(typeof(Title))
                       .Any(e => e.ToLowerInvariant().Equals(x.ToLowerInvariant()));
        }
    }
}
