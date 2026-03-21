using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace MyApp.WebApi.Exceptions.Handler
{
    using FluentValidation;
    using MyApp.WebApi.Exceptions.Models;

    internal sealed class ValidationExceptionHandler
        : BaseExceptionHandler<ValidationException>
    {
        public ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger)
            : base(logger) { }

        protected override ApiProblemDetails CreateProblemDetails(
            HttpContext context,
            ValidationException exception)
        {
            var errors = exception.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            return new ApiProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation Failed",
                ErrorCode = "VALIDATION_ERROR",
                ErrorMessage = "One or more validation errors occurred.",
                TraceId = context.TraceIdentifier,
                Errors = errors
            };
        }
    }


}
