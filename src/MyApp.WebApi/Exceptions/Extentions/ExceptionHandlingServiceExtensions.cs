

using MyApp.WebApi.Exceptions.Handler;

namespace MyApp.WebApi.Exceptions.Extentions
{

    public static class ExceptionHandlingServiceExtensions
    {
        public static IServiceCollection AddUnifiedExceptionHandling(this IServiceCollection services)
        {
            // Bật ProblemDetails (chuẩn RFC 7807)
            services.AddProblemDetails();

            // Cấu hình response cho validation tự động
            services.AddUnifiedValidationResponse();

            //Đăng ký các ExceptionHandler
            services.AddExceptionHandler<AuthorizeExceptionHandler>();
            services.AddExceptionHandler<ForbiddenExceptionHandler>();
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddExceptionHandler<InvalidOperationExceptionHandler>();
            services.AddExceptionHandler<BadRequestExceptionHandler>();
            services.AddExceptionHandler<NotFoundExceptionHandler>();
            services.AddExceptionHandler<IdentityResultExceptionHandler>();
            services.AddExceptionHandler<DatabaseExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }


}
