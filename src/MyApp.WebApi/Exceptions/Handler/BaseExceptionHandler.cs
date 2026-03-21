using Microsoft.AspNetCore.Diagnostics;
using MyApp.WebApi.Exceptions.Models;

namespace MyApp.WebApi.Exceptions.Handler
{
    // BaseExceptionHandler.cs
    public abstract class BaseExceptionHandler<TException> : IExceptionHandler
        where TException : Exception
    {
        private readonly ILogger _logger;

        protected BaseExceptionHandler(ILogger logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not TException typedException)
                return false;

            try
            {
                var problem = CreateProblemDetails(httpContext, typedException);

                // Nếu CreateProblemDetails trả về null → không xử lý, chuyển handler khác
                if (problem == null)
                    return false;

                httpContext.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

                _logger.LogError(exception, "Handled by {Handler}", GetType().Name);
            }
            catch (Exception writeEx)
            {
                _logger.LogError(writeEx, "Failed to write problem details response.");
                return false; // fallback cho GlobalHandler
            }

            return true;
        }


        protected abstract ApiProblemDetails CreateProblemDetails(HttpContext httpContext, TException exception);
    }

}
