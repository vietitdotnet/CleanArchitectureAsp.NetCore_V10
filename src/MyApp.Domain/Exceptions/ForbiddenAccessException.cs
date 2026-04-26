using MyApp.Domain.Exceptions.CodeErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Exceptions
{
    public class ForbiddenAccessException : BaseException
    {
        public ForbiddenAccessException(
               string message = "Cấm truy cập tài nguyên yêu cầu.",
               string errorCode = "Forbidden")
               : base(message, errorCode)
        {
        }

    }

}

