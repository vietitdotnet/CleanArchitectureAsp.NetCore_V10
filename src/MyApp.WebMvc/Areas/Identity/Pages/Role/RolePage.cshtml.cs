using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Application.Interfaces.Identity;

namespace MyApp.WebMvc.Areas.Identity.Pages.Role
{
    public class RolePageModel : PageModel
    {

        protected readonly ILogger<RolePageModel> _logger;

        protected IIDentityService _managerService;

        public RolePageModel(ILogger<RolePageModel> logger, IIDentityService managerService)
        {
            _logger = logger;
            _managerService = managerService;
        }

        [TempData]
        public string StatusMessage { get; set; } = string.Empty;

    
    }
}
