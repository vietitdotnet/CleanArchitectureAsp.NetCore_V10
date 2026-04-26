using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Application.Features.Identity.DTOs;
using MyApp.Application.Interfaces.Identity;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using MyApp.WebMvc.Areas.Identity.Pages.Role;

namespace MyApp.WebMvc.Areas.Identity.Pages.User
{
    public class IndexModel : UserPageModel
    {
        public IndexModel(ILogger<RolePageModel> logger,
            IIDentityService managerService) : base(logger, managerService)
        {
        }
        public PagedResponse<UserDto, UserParameters>? Users { get; set; }

        [BindProperty(SupportsGet = true)]
        public UserParameters Parameters { get; set; } = new();

        public async Task OnGetAsync()
        {
            Users = await _managerService.GetUsers(Parameters);
        }
    }
}
