using MyApp.Domain.Exceptions;
using MyApp.WebApi.Exceptions.Models;

namespace MyApp.WebApi.Exceptions.Handler
{
    internal sealed class InvalidOperationExceptionHandler
    : BaseExceptionHandler<InvalidOperationException>
    {
        public InvalidOperationExceptionHandler(ILogger<InvalidOperationException> logger)
            : base(logger) { }

        protected override ApiProblemDetails CreateProblemDetails(
            HttpContext context, InvalidOperationException exception)
        {
            return new ApiProblemDetails
            {
                Title = "Invalid Operation",
                Status = StatusCodes.Status500InternalServerError,
                ErrorCode = "Invalid_Operation",
                ErrorMessage = exception.Message,
                TraceId = context.TraceIdentifier
            };
        }
    }
}
