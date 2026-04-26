using Azure;
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
                            kvp => NormalizeKey(kvp.Key),
                            kvp => kvp.Value!.Errors
                                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage)
                                    ? "Giá trị không hợp lệ"
                                    : e.ErrorMessage)
                                .ToArray()
                        );

                    var problem = new ApiProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Validation Error",
                        ErrorCode = "Validation_Error",
                        ErrorMessage = "Dữ liệu không hợp lệ.",
                        Errors = errors,
                        TraceId = context.HttpContext.TraceIdentifier
                    };

                    return new BadRequestObjectResult(problem);
                };
            });

            return services;
        }

        private static string NormalizeKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return "general";

            // bỏ $. từ JSON path
            key = key.Replace("$.", "");

            // bỏ prefix "req." nếu có
            if (key.StartsWith("req.", StringComparison.OrdinalIgnoreCase))
                key = key.Substring(4);

            // camelCase
            return char.ToLowerInvariant(key[0]) + key.Substring(1);
        }
    }


}
