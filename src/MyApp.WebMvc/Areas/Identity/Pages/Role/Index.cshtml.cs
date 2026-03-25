using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Interfaces.Identity;

namespace MyApp.WebMvc.Areas.Identity.Pages.Role
{
    public class IndexModel : RolePageModel
    {
        public IndexModel(
            ILogger<RolePageModel> logger,
            IIDentityService managerService) : base(logger, managerService)
        {
        }

        public void OnGet()
        {
        }
    }
}
