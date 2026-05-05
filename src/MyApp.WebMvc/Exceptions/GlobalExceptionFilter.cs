using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MyApp.Domain.Exceptions;
using MyApp.WebMvc.Areas.Manager.Models;
using MyApp.WebMvc.Models;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public const string PageErrorViewPath = "~/Views/Shared/PageError.cshtml";
    public const string ErrorViewPath = "~/Views/Shared/Error.cshtml";

    public GlobalExceptionFilter(
        IWebHostEnvironment env,
        ILogger<GlobalExceptionFilter> logger)
    {
        _env = env;
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {

            case BadRequestException bex:
                HandleError(context, StatusCodes.Status400BadRequest, bex.ErrorCode,  bex.Message);
                break;

            case NotFoundException nex:
                HandleError(context, StatusCodes.Status404NotFound, nex.ErrorCode, nex.Message);
                break;

            case UnauthorizedAccessException:
                context.Result = new RedirectToPageResult("/Account/Login");
                context.ExceptionHandled = true;
                break;

            case ForbiddenAccessException fex:
                HandleError(context, StatusCodes.Status403Forbidden, fex.ErrorCode, fex.Message);
                break;

            default:
                HandleInternalServerError(context);
                break;
        }
    }

    
   
    private void HandleInternalServerError(ExceptionContext context)
    {
        var exception = context.Exception;

        _logger.LogError(exception, "Unhandled Exception");

        var model = new ErrorViewModel
        {
            RequestId = context.HttpContext.TraceIdentifier,
            ShowRequestId = true,

            StatusCode = StatusCodes.Status500InternalServerError,

            ErrorCode = "ERR-500",

            // bạn có thể thêm field Message nếu muốn custom
            Message = _env.IsDevelopment()
                ? exception.ToString()
                : "Đã xảy ra lỗi hệ thống. Vui lòng thử lại sau."
        };

        context.Result = new ViewResult
        {
            ViewName = ErrorViewPath,
            ViewData = new ViewDataDictionary<ErrorViewModel>(
                new EmptyModelMetadataProvider(),
                context.ModelState)
            {
                Model = model
            }
        };

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.ExceptionHandled = true;
    }



    private void HandleError(
    ExceptionContext context,
    int statusCode,
    string errorCode,
    string message)
    {
        var model = new ErrorViewModel
        {
            RequestId = context.HttpContext.TraceIdentifier,
            ShowRequestId = true,
            Message = message,
            StatusCode = statusCode,
            ErrorCode = errorCode
        };

        context.Result = new ViewResult
        {
            ViewName = PageErrorViewPath, // 👈 chỉ 1 view
            ViewData = new ViewDataDictionary<ErrorViewModel>(
                new EmptyModelMetadataProvider(),
                context.ModelState)
            {
                Model = model
            }
        };

        context.HttpContext.Response.StatusCode = statusCode;
        context.ExceptionHandled = true;
    }
}
