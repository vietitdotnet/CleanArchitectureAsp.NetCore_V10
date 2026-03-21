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
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                ErrorCode = ErrorCodeCategories.Unauthorized,
                ErrorMessage = exception.Message,
                TraceId = context.TraceIdentifier
            };
        }
    }
}
