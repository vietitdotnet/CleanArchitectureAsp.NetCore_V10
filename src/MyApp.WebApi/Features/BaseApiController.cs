using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Application.Common.Results;
using MyApp.Application.Features.PromotionItems.Requests;
using MyApp.Application.Features.PromotionItems.Respones;
using MyApp.WebApi.Exceptions.Models;

namespace MyApp.WebApi.Features
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected ActionResult BadRequestResult<T>(OperationResult<T> result)
        {
            var details = new ApiProblemDetails
            {
                Title = "Bad Request",
                Status = StatusCodes.Status400BadRequest,
                ErrorCode = "Bad_Request",
                ErrorMessage = result.Message ?? "One or more errors occurred.",
                Errors = result.Errors,
                TraceId = HttpContext.TraceIdentifier
            };

            return BadRequest(details);
        }


    }

}