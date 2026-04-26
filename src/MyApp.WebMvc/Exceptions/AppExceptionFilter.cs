using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MyApp.Domain.Exceptions;


public class AppExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ValidationException vex:
                HandleValidation(context, vex.Errors.Select(e => e.ErrorMessage));
                break;

            case BadRequestException bex:
                HandleValidation(context, new[] { bex.Message });
                break;

            case IdentityResultException iex:
                HandleValidation(context, iex.Errors);
                break;

            case NotFoundException nex:
                HandleNotFound(context, nex.Message);
                break;

            case UnauthorizedAccessException:
                context.Result = new RedirectToPageResult("/Account/Login");
                context.ExceptionHandled = true;
                break;

            case ForbiddenAccessException:
                context.Result = new RedirectToPageResult("/Forbidden");
                context.ExceptionHandled = true;
                break;

            default:
                break;
        }
    }

    private void HandleValidation(ExceptionContext context, IEnumerable<string> errors)
    {
        foreach (var error in errors)
        {
            context.ModelState.AddModelError("", error);
        }

        if (context.ActionDescriptor is CompiledPageActionDescriptor)
        {
            context.Result = new PageResult();
        }
        else
        {
            context.Result = new ViewResult
            {
                ViewName = context.RouteData.Values["action"]?.ToString()
            };
        }

        context.ExceptionHandled = true;
    }

    private void HandleNotFound(ExceptionContext context, string message)
    {
        context.Result = new ViewResult
        {
            ViewName = "~/Views/Shared/NotFound.cshtml",
            ViewData = new ViewDataDictionary(
                new EmptyModelMetadataProvider(),
                new ModelStateDictionary())
            {
                Model = message
            }
        };

        context.ExceptionHandled = true;
    }
}
