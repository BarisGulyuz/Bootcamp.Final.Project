using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleperformance.Core.Utilities.Results
{
    public class Result : IResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<CustomValidationError> ValidationErrors { get; set; } = new List<CustomValidationError>();
        public static Result Success()
        {
            return new Result
            {
                IsSuccess = true,
            };
        }

        public static Result Success(string message)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message
            };
        }
        public static Result Failure(string message)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message
            };
        }
        public static Result Failure(string message, List<CustomValidationError> validationErrors)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message,
                ValidationErrors = validationErrors
            };
        }
    }
}

