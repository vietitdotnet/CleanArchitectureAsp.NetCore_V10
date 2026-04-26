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
            if (result.Errors == null) return;

            foreach (var (field, messages) in result.Errors)
                foreach (var message in messages)
                    modelState.AddModelError(
                        field == "general" ? string.Empty : field,
                        message
                    );
        }
    }
}
