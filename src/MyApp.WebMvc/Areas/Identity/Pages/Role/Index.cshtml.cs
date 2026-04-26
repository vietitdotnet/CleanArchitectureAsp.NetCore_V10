using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Application.Features.Identity.DTOs;
using MyApp.Application.Interfaces.Identity;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;

namespace MyApp.WebMvc.Areas.Identity.Pages.Role
{
    public class IndexModel : RolePageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public IndexModel(
            ILogger<RolePageModel> logger,
            IIDentityService managerService,
            RoleManager<IdentityRole> roleManager) : base(logger, managerService)
        {
            _roleManager = roleManager;
        }

        public PagedResponse<RoleDto, RoleParameters> RoleClamis { get; private set; } = null!; 

        public async Task OnGet([FromQuery]RoleParameters param)
        {
            RoleClamis = await _identityService.GetRoles(param);
        }
    }
}
