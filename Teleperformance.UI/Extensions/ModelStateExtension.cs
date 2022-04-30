using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teleperformance.Core.Utilities.Results;

namespace Teleperformance.UI.Extensions
{
    public static class ModelStateExtension
    {
        public static void AddCustomErros(this ModelStateDictionary modelState, List<CustomValidationError> customValidations)
        {
            foreach (var item in customValidations)
            {
                modelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
        }
    }
}
