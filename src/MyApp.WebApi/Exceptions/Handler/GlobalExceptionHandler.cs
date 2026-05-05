using Microsoft.EntityFrameworkCore;
using MyApp.WebApi.Exceptions.Models;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace MyApp.WebApi.Exceptions.Handler
{
    internal sealed class GlobalExceptionHandler : BaseExceptionHandler<Exception>
    {
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
            : base(logger)
        {
        }

        protected override ApiProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
        {
            return new ApiProblemDetails
            {
                Title = "Server Error",
                Status = StatusCodes.Status500InternalServerError,     
                ErrorCode = "INTERNAL_SERVER_ERROR",
                ErrorMessage = "Lỗi hệ thống, vui lòng thử lại sau.",
                TraceId = context.TraceIdentifier
            };
        }
    }

}