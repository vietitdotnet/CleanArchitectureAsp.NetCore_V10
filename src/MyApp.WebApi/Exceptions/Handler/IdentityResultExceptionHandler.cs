using MyApp.Domain.Exceptions;
using MyApp.WebApi.Exceptions.Models;

namespace MyApp.WebApi.Exceptions.Handler
{
    internal sealed class IdentityResultExceptionHandler
    : BaseExceptionHandler<IdentityResultException>
    {
        public IdentityResultExceptionHandler(ILogger<NotFoundExceptionHandler> logger)
            : base(logger) { }

        protected override ApiProblemDetails CreateProblemDetails(
            HttpContext context, IdentityResultException exception)
        {
            return new ApiProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request Error",
                ErrorCode = exception.Codes.FirstOrDefault() ?? exception.ErrorCode,
                ErrorMessage = exception.Errors.FirstOrDefault() ?? exception.Message,
                TraceId = context.TraceIdentifier,
               
            };
        }

      
    }
}
