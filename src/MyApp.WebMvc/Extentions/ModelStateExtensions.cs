using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyApp.Application.Common.Results;

namespace MyApp.WebMvc.Extentions
{
    public static class ModelStateExtensions
    {
        public static void AddErrors<T>(
        this ModelStateDictionary modelState,
        OperationResult<T> result)
        {
            if (result.Errors == null)
                modelState.AddModelError(string.Empty, result.Message!);

            if (result.Errors != null)
            foreach (var (field, messages) in result.Errors)
                foreach (var message in messages)
                    modelState.AddModelError(
                        field == "general" ? string.Empty : field,
                        message
                    );

            return;
        }
    }
}
