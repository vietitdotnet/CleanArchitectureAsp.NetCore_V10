using MyApp.Application.Common.Results;
using MyApp.Application.Features.Authentications.DTOs;
using MyApp.Application.Features.Authentications.Requests;
using MyApp.Application.Features.Authentications.Responses;


namespace MyApp.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<OperationResult<AuthUserDto>> LoginAsync(LoginRequest request);
        Task<OperationResult<AuthUserDto>> RegisterAsync(RegisterRequest request);
        Task<OperationResult<AuthUserDto>> RefreshTokenAsync(RefreshTokenRequest request);
        Task<OperationResult<bool>> LogoutAllAsync(string userId);
        Task<OperationResult<bool>> LogoutAsync(string refreshToken);
        Task<OperationResult<bool>> ConfirmEmailAsync(string userId, string code);

        Task<OperationResult<AuthUserDto>> ExternalCallbackLoginAsync();

    }
}
