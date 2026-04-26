using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Application.Interfaces.Identity;
using MyApp.WebMvc.Areas.Identity.Pages.Role;

namespace MyApp.WebMvc.Areas.Identity.Pages.User
{
    public class UserPageModel : PageModel
    {
        protected readonly ILogger<RolePageModel> _logger;

        protected IIDentityService _managerService;

        public UserPageModel(ILogger<RolePageModel> logger, IIDentityService managerService)
        {
            _logger = logger;
            _managerService = managerService;
        }

        [TempData]
        public string StatusMessage { get; set; } = string.Empty;
    }
}
