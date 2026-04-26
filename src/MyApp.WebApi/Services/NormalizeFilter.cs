using Microsoft.AspNetCore.Mvc.Filters;
using MyApp.Domain.Paginations.Interfaces;
using Org.BouncyCastle.Pqc.Crypto.Bike;

namespace MyApp.WebApi.Services
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
