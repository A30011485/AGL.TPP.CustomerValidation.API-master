using System.Collections.Generic;
using System.Linq;
using AGL.TPP.CustomerValidation.API.Models;
using FluentValidation.Results;

namespace AGL.TPP.CustomerValidation.API
{
    public static class FluentValidationExtensions
    {
        public static List<FieldLevelError> GetFieldLevelErrors(this ValidationResult validationResult)
        {
            var errorsList = new List<FieldLevelError>();

            var errorMessages = validationResult.Errors;
            foreach (var errorMessage in errorMessages)
            {
                errorsList.Add(new FieldLevelError
                {
                    Field = errorMessage.PropertyName,
                    Code = errorMessage.ErrorCode,
                    Message = errorMessage.ErrorMessage
                });
            }

            return errorsList;
        }

        public static List<string> GetErrorMessages(this ValidationResult validationResult)
        {
            return validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        }
    }
}
