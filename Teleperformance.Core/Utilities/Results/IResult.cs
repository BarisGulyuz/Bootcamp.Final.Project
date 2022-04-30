using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleperformance.Core.Utilities.Results
{
    public interface IResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<CustomValidationError> ValidationErrors { get; set; } 
    }
}
