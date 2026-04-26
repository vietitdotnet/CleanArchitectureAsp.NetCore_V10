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
         
            var role = new IdentityRole(Input.Name);

            var result = await _identityService.CreateRoleAsync(Input);

            if (result.Success)
            {
                StatusMessage = $"Thêm vai trò {role.Name} thành công.";

                return RedirectToPage("./Index");
            }

            foreach (var kvp in result.Errors!)
            {
                var field = kvp.Key;
                var messages = kvp.Value;

                foreach (var message in messages)
                {
                    ModelState.AddModelError(
                        field == "general" ? string.Empty : field,
                        message
                    );
                }
            }

            return Page();
        }
    }
}
