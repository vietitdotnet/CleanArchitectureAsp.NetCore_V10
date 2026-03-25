
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Authentications.Requests;
using MyApp.Application.Features.Authentications.Responses;
using MyApp.Application.Interfaces.Auth;
using MyApp.Application.Interfaces.External;
using MyApp.Infrastructure.Entities.Identity;

namespace MyApp.WebApi.Features.Auths
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly IEmailService _emailService;
        public AuthController(IAuthService authService, SignInManager<AppUser> signInManager , IEmailService emailService)
        {
            _authService = authService;
            _signInManager = signInManager;
            _emailService = emailService;
            
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RegisterResponse>> Register(
            [FromBody] RegisterRequest request,
            CancellationToken ct)
        {
            var result = await _authService.RegisterAsync(request);

            return Ok(new RegisterResponse
            {
                Data = result.Data
            });
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponse>> Login(
            [FromBody] LoginRequest request,
            CancellationToken ct)
        {
            var result = await _authService.LoginAsync(request);

            return Ok(new LoginResponse
            {
                Data = result.Data
            });
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var result = await _authService.RefreshTokenAsync(request);
            return Ok(new { result.Success, result.Message });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            var result = await _authService.LogoutAsync(request.RefreshToken);
            return Ok(new { result.Success, result.Message });
        }

        [HttpPost("logout-all")]
        public async Task<IActionResult> AllLogout([FromBody] LogoutAllRequest request)
        {
            var result = await _authService.LogoutAllAsync(request.UserId);
            return Ok(result.Data);
        }

        [HttpGet("external-login")]
        public IActionResult ExternalLogin([FromQuery] string provider)
        {
            if (string.IsNullOrEmpty(provider))
                return BadRequest("Provider is required");

            var redirectUrl = Url.Action(nameof(ExternalCallback), "Auth", null, Request.Scheme);


            var properties = _signInManager
                .ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return Challenge(properties, provider);
        }


        [HttpGet("callback")]
        public async Task<IActionResult> ExternalCallback()
        {
            var result = await _authService.ExternalCallbackLoginAsync();
            return Ok(result.Data);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string code)
        {
            var result = await _authService
                .ConfirmEmailAsync(userId, code);

            return Ok(result.Data);
        }
    }

}
