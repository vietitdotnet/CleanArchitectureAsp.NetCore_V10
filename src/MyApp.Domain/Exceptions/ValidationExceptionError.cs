using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyApp.Domain.Exceptions
{
    public class ValidationExceptionError : BaseException
    {
        public Dictionary<string, string[]> Errors { get; set; }
        public ValidationExceptionError(
            Dictionary<string, string[]>? errors = null,
            string message = "Dữ liệu không hợp lệ",
            string errorCode = "Validation"
            ) :
            base(message, errorCode)
        {
            Errors = errors ?? new Dictionary<string, string[]>();
        }
    }
}
