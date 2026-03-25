using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Identity.Requests;
using MyApp.Application.Interfaces.Identity;


namespace MyApp.WebMvc.Areas.Identity.Pages.Role
{
    public class CreateModel : RolePageModel
    {
        public CreateModel(ILogger<RolePageModel> logger, IIDentityService managerService) : base(logger, managerService)
        {
        }

        public class InputModel : CreateRoleRequest
        {

        }

        [BindProperty]
        public InputModel Input { get; set; } = null!;
        
        public IActionResult OnGet()
        {
            Input = new InputModel();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole(Input.Name);

                var result = await _managerService.CreateRoleAsync(Input);

                if (result.Success)
                {
                    StatusMessage = $"Thêm vai trò {role.Name} thành công.";

                    return RedirectToPage("./Index");
                }

                foreach (var error in result.Errors!)
                {
                    ModelState.AddModelError(string.Empty, error);

                }
            }
            return Page();
        }
    }
}
