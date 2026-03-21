using MyApp.Domain.Exceptions;
using MyApp.WebApi.Exceptions.Models;


namespace MyApp.WebApi.Exceptions.Handler
{
    internal sealed class NotFoundExceptionHandler
    : BaseExceptionHandler<NotFoundException>
    {
        public NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger)
            : base(logger) { }

        protected override ApiProblemDetails CreateProblemDetails(
            HttpContext context, NotFoundException exception)
        {
            return new ApiProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                ErrorCode = exception.ErrorCode,
                ErrorMessage = exception.Message,
                TraceId = context.TraceIdentifier
            };
        }
    }
}
