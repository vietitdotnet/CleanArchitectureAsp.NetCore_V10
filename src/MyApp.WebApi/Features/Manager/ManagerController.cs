using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Abstractions.Services;
using MyApp.Application.Features.Managers.Responses;
using MyApp.Application.Features.Orders.Responses;
using MyApp.Domain.Paginations.Parameters;

namespace MyApp.WebApi.Features.Manager
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(GetUsersResponse), StatusCodes.Status200OK)]
        public async Task<GetUsersResponse> GetUsers([FromQuery]UserParameters parameters)
        {
            parameters.Normalize();

            var result = await _managerService.GetUsers(parameters);

            return new GetUsersResponse(result);
           
        }
    }
}
