using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Utilities.Results;

namespace Teleperformance.Business.Extensions
{
    public static class FluentValidationExtension
    {
        public static List<CustomValidationError> ConvertToCustomValidationErrors(this ValidationResult validationResult)
        {
            List<CustomValidationError> customValidationErrors = new();
            foreach (var error in validationResult.Errors)
            {
                customValidationErrors.Add(new CustomValidationError
                {
                    ErrorMessage = error.ErrorMessage,
                    PropertyName = error.PropertyName
                });
            }
            return customValidationErrors;
        }
    }
}
