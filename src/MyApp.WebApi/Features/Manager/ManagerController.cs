
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Identity.Requests;
using MyApp.Application.Features.Identity.Responses;
using MyApp.Application.Interfaces.Identity;
using MyApp.Domain.Paginations.Parameters;

namespace MyApp.WebApi.Features.Manager
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IIDentityService _identityService;

        public ManagerController(IIDentityService dentityService)
        {
            _identityService = dentityService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(GetUsersResponse), StatusCodes.Status200OK)]
        public async Task<GetUsersResponse> GetUsers([FromQuery]UserParameters parameters)
        {
            parameters.Normalize();

            var result = await _identityService.GetUsers(parameters);

            return new GetUsersResponse(result);
           
        }
       

        [HttpPost]
        [ProducesResponseType(typeof(CreateRoleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateRoleResponse>> CreateRole(
            [FromBody] CreateRoleRequest req)
        {
           var result = await _identityService.CreateRoleAsync(req);

           return StatusCode(201, new CreateRoleResponse(result));
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetRolesResponse), StatusCodes.Status200OK)]
        public async Task<GetRolesResponse> GetRoles([FromQuery] RoleParameters param)
        {
            param.Normalize();

            var result = await _identityService.GetRoles(param);

            return new GetRolesResponse(result);

        }
    }
}
