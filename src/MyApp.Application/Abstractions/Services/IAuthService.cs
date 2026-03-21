using MyApp.Application.Common.Models;
using MyApp.Application.Features.Authentications.DTOs;
using MyApp.Application.Features.Authentications.Requests;
using MyApp.Application.Features.Authentications.Responses;


namespace MyApp.Application.Abstractions.Services
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
