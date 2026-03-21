using Microsoft.AspNetCore.Mvc;
using MyApp.Domain.Exceptions.CodeErrors;
using MyApp.WebApi.Exceptions.Models;

namespace MyApp.WebApi.Exceptions.Extentions
{
    public static class ValidationBehaviorExtensions
    {
        public static IServiceCollection AddUnifiedValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var problem = new ApiProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Validation Error",
                        ErrorCode = ErrorCodeCategories.Validation,
                        ErrorMessage = "Dữ liệu không hợp lệ.",
                        Errors = errors,
                        TraceId = context.HttpContext.TraceIdentifier
                    };

                    return new BadRequestObjectResult(problem);
                };
            });

            return services;
        }
    }

}
