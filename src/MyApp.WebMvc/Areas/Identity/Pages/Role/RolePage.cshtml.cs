using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Application.Interfaces.Identity;

namespace MyApp.WebMvc.Areas.Identity.Pages.Role
{
    public class RolePageModel : PageModel
    {

        protected readonly ILogger<RolePageModel> _logger;

        protected IIDentityService _identityService;

        public RolePageModel(ILogger<RolePageModel> logger, IIDentityService identityService)
        {
            _logger = logger;
            _identityService = identityService;
        }

        [TempData]
        public string StatusMessage { get; set; } = string.Empty;

    
    }
}
