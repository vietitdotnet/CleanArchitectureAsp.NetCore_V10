using MyApp.Domain.Exceptions.CodeErrors;
using MyApp.WebApi.Exceptions.Models;

namespace MyApp.WebApi.Exceptions.Handler
{
    internal sealed class AuthorizeExceptionHandler
        : BaseExceptionHandler<UnauthorizedAccessException>
    {
        public AuthorizeExceptionHandler(ILogger<AuthorizeExceptionHandler> logger)
            : base(logger) { }

        protected override ApiProblemDetails CreateProblemDetails(HttpContext context, UnauthorizedAccessException exception)
        {
            return new ApiProblemDetails
            {
                Title = "Unauthorized",
                Status = StatusCodes.Status401Unauthorized,
                ErrorCode = "Unauthorized",
                ErrorMessage = exception.Message,
                TraceId = context.TraceIdentifier
            };
        }
    }
}
