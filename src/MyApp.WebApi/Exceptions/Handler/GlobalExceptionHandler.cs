using MyApp.WebApi.Exceptions.Models;

namespace MyApp.WebApi.Exceptions.Handler
{
    internal sealed class GlobalExceptionHandler : BaseExceptionHandler<Exception>
    {
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
            : base(logger)
        {
        }

        protected override ApiProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
        {
            return new ApiProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error",
                ErrorCode = "INTERNAL_SERVER_ERROR",
                ErrorMessage = "Lỗi hệ thống, vui lòng thử lại sau.",
                TraceId = context.TraceIdentifier
            };
        }
    }

}


