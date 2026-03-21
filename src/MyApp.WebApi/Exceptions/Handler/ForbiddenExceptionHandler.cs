using MyApp.Domain.Exceptions;
using MyApp.Domain.Exceptions.CodeErrors;
using MyApp.WebApi.Exceptions.Models;

namespace MyApp.WebApi.Exceptions.Handler
{
    internal sealed class ForbiddenExceptionHandler
         : BaseExceptionHandler<ForbiddenAccessException>
    {
        public ForbiddenExceptionHandler(ILogger<ForbiddenExceptionHandler> logger)
            : base(logger) { }

        protected override ApiProblemDetails CreateProblemDetails(HttpContext context, ForbiddenAccessException exception)
        {
            return new ApiProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                ErrorCode = ErrorCodeCategories.Forbidden,
                ErrorMessage = exception.Message,
                TraceId = context.TraceIdentifier
            };
        }
    }
}
