using Microsoft.AspNetCore.Mvc.Filters;
using MyApp.Domain.Paginations.Interfaces;

namespace MyApp.WebMvc.Services
{
    public class NormalizeFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument is INormalizable normalizable)
                {
                    normalizable.Normalize();
                }
            }
            await next();
        }
    }
}
