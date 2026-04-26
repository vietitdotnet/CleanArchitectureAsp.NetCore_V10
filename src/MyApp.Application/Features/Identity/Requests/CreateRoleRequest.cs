using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.Features.Identity.Requests
{
    public class CreateRoleRequest
    {
        [Display(Name = "Tên")]
        public string Name { get; set; } = null!;
    }
}
