
using MyApp.Domain.Exceptions;
using MyApp.WebApi.Exceptions.Models;

namespace MyApp.WebApi.Exceptions.Handler
{
    internal sealed class BadRequestExceptionHandler
     : BaseExceptionHandler<BadRequestException>
    {
        public BadRequestExceptionHandler(ILogger<BadRequestExceptionHandler> logger)
            : base(logger) { }

        protected override ApiProblemDetails CreateProblemDetails(
            HttpContext context, BadRequestException exception)
        {
            return new ApiProblemDetails
            {
                Title = "Bad Request Error",
                ErrorMessage = exception.Message,
                Status = StatusCodes.Status400BadRequest,
                ErrorCode = exception.ErrorCode,
                TraceId = context.TraceIdentifier
            };
        }


    }
}
