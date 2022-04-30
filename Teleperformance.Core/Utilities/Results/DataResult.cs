using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleperformance.Core.Utilities.Results
{
    public class DataResult<T> : IResult
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; } = default!;
        public string Message { get; set; }
        public List<CustomValidationError> ValidationErrors { get; set; } = new List<CustomValidationError>();

        public static DataResult<T> Success(T data)
        {
            return new DataResult<T>
            {
                IsSuccess = true,
                Data = data
            };
        }
        public static DataResult<T> Success(T data, string message)
        {
            return new DataResult<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message
            };
        }
        public static DataResult<T> Failure(string message)
        {
            return new DataResult<T>
            {
                IsSuccess = false,
                Message = message
            };
        }
        public static DataResult<T> Failure(string message, List<CustomValidationError> validationErrors)
        {
            return new DataResult<T>
            {
                IsSuccess = false,
                Message = message,
                ValidationErrors = validationErrors
            };
        }
    }
}
