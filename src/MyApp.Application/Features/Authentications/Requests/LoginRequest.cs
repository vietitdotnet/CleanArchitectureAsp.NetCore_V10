
namespace MyApp.Application.Features.Authentications.Requests
{
    public class LoginRequest
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; } = false;
    }
}
