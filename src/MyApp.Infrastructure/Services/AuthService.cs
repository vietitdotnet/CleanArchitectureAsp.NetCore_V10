using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyApp.Application.Abstractions.Services;
using MyApp.Application.Common.Models;
using MyApp.Application.Features.Authentications.DTOs;
using MyApp.Application.Features.Authentications.Requests;
using MyApp.Domain.Entities;
using MyApp.Domain.Exceptions;
using MyApp.Infrastructure.Constants;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Models;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;


namespace MyApp.Infrastructure.Services.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly MyAppDbContext _context;
        private readonly ILogger<AuthService> _logger;

        private readonly IHashService _hashService;
        private readonly double _accessTokenExpirySeconds;

        private readonly IRequestInfoService _requestInfoService;

        public AuthService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IJwtService jwtService,
            IConfiguration configuration,
            ILogger<AuthService> logger,
            MyAppDbContext myAppDbContext,
            IHashService hashService,
            IRequestInfoService requestInfoService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _context = myAppDbContext;
            _logger = logger;

            _accessTokenExpirySeconds = _jwtService.GetAccessTokenExpirySeconds();
            _hashService = hashService;
            _requestInfoService = requestInfoService;
        }

        public async Task<OperationResult<AuthUserDto>> LoginAsync(LoginRequest request)
        {
            // 1. Tìm user
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                throw new NotFoundException("Email hoặc mật khẩu không đúng.");

            // 2. Check lockout
            if (await _userManager.IsLockedOutAsync(user))
                throw new BadRequestException("Tài khoản đã bị khóa do đăng nhập sai nhiều lần.");

            // 3. Check password
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (!result.Succeeded)
                throw new BadRequestException("Email hoặc mật khẩu không đúng.");

            // 4. Check email confirmed (optional nhưng nên có)
            if (!user.EmailConfirmed)
                throw new BadRequestException("Email chưa được xác nhận.");

            // 5. Lấy roles
            var roles = await _userManager.GetRolesAsync(user);

            // 6. Generate tokens
            var accessToken = _jwtService.GenerateAccessToken(user, roles);
            var refreshToken = _jwtService.GenerateRefreshToken();
            var hashedToken = _hashService.GetHash(refreshToken);

            // 7. Lấy thông tin request (device, ip, agent)
            var device = _requestInfoService.GetDevice();
            var ipAddress = _requestInfoService.GetIpAddress();
            var userAgent = _requestInfoService.GetUserAgent();

            // 8. Revoke token cùng device (multi-device support)
            var oldTokens = await _context.AutUserUserTokens
                .Where(x => x.UserId == user.Id &&
                            x.Device == device &&
                            !x.IsRevoked)
                .ToListAsync();

            foreach (var token in oldTokens)
            {
                token.Revoke();
            }

            // 9. Tạo refresh token mới
            var userToken = AutUserToken.Create(
                user.Id,
                hashedToken,
                DateTime.UtcNow.AddDays(7),
                device,
                ipAddress,
                userAgent
            );

            _context.AutUserUserTokens.Add(userToken);

            // 10. Cleanup token hết hạn (tránh phình DB)
            await _context.AutUserUserTokens
                .Where(x => x.ExpiryTime < DateTime.UtcNow)
                .ExecuteDeleteAsync();

            await _context.SaveChangesAsync();

            _logger.LogInformation("Dang nhap thanh cong");
            // 11. Return
            return OperationResult<AuthUserDto>.Ok(
                AuthUserDto.Create(
                    user.Id!,
                    user.Email!,
                    $"{user.FirstName} {user.LastName}".Trim(),
                    roles.ToList(),
                    accessToken,
                    refreshToken,
                    _accessTokenExpirySeconds,
                    device,
                    ipAddress,
                    userAgent
                )
            );
        }

        public async Task<OperationResult<bool>> LogoutAsync(string refreshToken)
        {
            var hashedToken = _hashService.GetHash(refreshToken);

            var storedToken = await _context.AutUserUserTokens
                .FirstOrDefaultAsync(x => x.RefreshToken == hashedToken);

            if (storedToken == null)
                return OperationResult<bool>.Ok(true, "Đã logout.");

            storedToken.Revoke();

            await _context.SaveChangesAsync();

            return OperationResult<bool>.Ok(true, "Logout thành công.");
        }

        public async Task<OperationResult<bool>> LogoutAllAsync(string userId)
        {
            var tokens = await _context.AutUserUserTokens
                .Where(x => x.UserId == userId && !x.IsRevoked)
                .ToListAsync();

            foreach (var token in tokens)
            {
                token.Revoke();
            }

            await _context.SaveChangesAsync();

            return OperationResult<bool>.Ok(true, "Đã logout tất cả thiết bị.");
        }


        public async Task<OperationResult<AuthUserDto>> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(request.Token);

            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                throw new UnauthorizedAccessException("UserId không hợp lệ.");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UnauthorizedAccessException("Người dùng không tồn tại.");

            var hashedRequestToken = _hashService.GetHash(request.RefreshToken);

            var storedToken = await _context.AutUserUserTokens
                .FirstOrDefaultAsync(x =>
                    x.RefreshToken == hashedRequestToken &&
                    x.UserId == userId);

            if (storedToken == null)
                throw new UnauthorizedAccessException("Refresh token không tồn tại.");

            //  detect reuse attack
            if (storedToken.IsRevoked)
            {
                await RevokeAllUserTokens(userId);
                throw new UnauthorizedAccessException("Token đã bị sử dụng lại (possible attack).");
            }

            if (storedToken.ExpiryTime <= DateTime.UtcNow)
                throw new UnauthorizedAccessException("Refresh token đã hết hạn.");

            //  revoke token cũ
            storedToken.Revoke();

            var roles = await _userManager.GetRolesAsync(user);

            var newAccessToken = _jwtService.GenerateAccessToken(user, roles);
            var newRefreshToken = _jwtService.GenerateRefreshToken();
            var hashedNewToken = _hashService.GetHash(newRefreshToken);

            //  giữ nguyên device info
            var newUserToken = AutUserToken.Create(
                user.Id,
                hashedNewToken,
                DateTime.UtcNow.AddDays(7),
                storedToken.Device,
                storedToken.IpAddress,
                storedToken.UserAgent
            );

            _context.AutUserUserTokens.Add(newUserToken);

            //  cleanup DB
            await _context.AutUserUserTokens
                .Where(x => x.ExpiryTime < DateTime.UtcNow)
                .ExecuteDeleteAsync();

            await _context.SaveChangesAsync();

            return OperationResult<AuthUserDto>.Ok(
                AuthUserDto.Create(
                    user.Id!,
                    user.Email!,
                    $"{user.FirstName} {user.LastName}".Trim(),
                    roles.ToList(),
                    newAccessToken,
                    newRefreshToken,
                    _accessTokenExpirySeconds,
                    newUserToken.Device,
                     newUserToken.IpAddress,
                    newUserToken.UserAgent          
                )
            );
        }


        public async Task<OperationResult<AuthUserDto>> RegisterAsync(RegisterRequest request)
        {
            if (request.Password != request.ConfirmPassword)
                throw new BadRequestException("Password không khớp");

            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
                throw new BadRequestException("Email đã tồn tại");

            var user = new AppUser
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true // bypass confirm
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new BadRequestException(
                    string.Join("; ", result.Errors.Select(e => e.Description))
                );


            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = _jwtService.GenerateAccessToken(user, roles);
            var refreshToken = _jwtService.GenerateRefreshToken();
            var hashedToken = _hashService.GetHash(refreshToken);

            var device = _requestInfoService.GetDevice();
            var ip = _requestInfoService.GetIpAddress();
            var agent = _requestInfoService.GetUserAgent();

            var userToken = AutUserToken.Create(
                user.Id,
                hashedToken,
                DateTime.UtcNow.AddDays(7),
                device,
                ip,
                agent
            );

            _context.AutUserUserTokens.Add(userToken);

            await _context.SaveChangesAsync();

            return OperationResult<AuthUserDto>.Ok(
                AuthUserDto.Create(
                    user.Id!,
                    user.Email!,
                    $"{user.FirstName} {user.LastName}".Trim(),
                    roles.ToList(),
                    accessToken,
                    refreshToken,
                    _accessTokenExpirySeconds,
                    device,
                    ip,
                    agent
                )
            );
        }


        public async Task<OperationResult<AuthUserDto>> ExternalCallbackLoginAsync()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
                throw new UnauthorizedAccessException("Không lấy được thông tin đăng nhập ngoài");

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var fullName = info.Principal.FindFirstValue(ClaimTypes.Name) ?? "";

            if (string.IsNullOrEmpty(email))
                throw new UnauthorizedAccessException("Provider không cung cấp email");

            // 1. Tìm user theo email
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new AppUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = ExtractFirstName(fullName),
                    LastName = ExtractLastName(fullName),
                    EmailConfirmed = true // external login → trust luôn
                };

                var createResult = await _userManager.CreateAsync(user);

                if (!createResult.Succeeded)
                    throw new BadRequestException(string.Join("; ", createResult.Errors.Select(x => x.Description)));
            }
            else
            {
                if (!user.EmailConfirmed)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                }
            }

            // 2. Check login đã tồn tại chưa
            var logins = await _userManager.GetLoginsAsync(user);

            if (!logins.Any(x =>
                x.LoginProvider == info.LoginProvider &&
                x.ProviderKey == info.ProviderKey))
            {
                var addLoginResult = await _userManager.AddLoginAsync(user, info);

                if (!addLoginResult.Succeeded)
                    throw new BadRequestException("Không thể liên kết tài khoản external");
            }

            // 3. Lấy roles
            var roles = await _userManager.GetRolesAsync(user);

            // 4. Generate tokens
            var accessToken = _jwtService.GenerateAccessToken(user, roles);
            var refreshToken = _jwtService.GenerateRefreshToken();
            var hashedToken = _hashService.GetHash(refreshToken);

            // 5. Lưu refresh token (hash)
            var newUserToken = AutUserToken.Create(
                user.Id,
                hashedToken,
                DateTime.UtcNow.AddDays(7)
            );

            _context.AutUserUserTokens.Add(newUserToken);

            await _context.SaveChangesAsync();

            // 6. Return
            return OperationResult<AuthUserDto>.Ok(
                AuthUserDto.Create(
                    user.Id!,
                    user.Email!,
                    $"{user.FirstName} {user.LastName}".Trim(),
                    roles.ToList(),
                    accessToken,
                    refreshToken,
                    _accessTokenExpirySeconds
                )
            );
        }


        public async Task<OperationResult<bool>> ConfirmEmailAsync(string userId, string code)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return OperationResult<bool>.Fail("UserId không hợp lệ.");

            if (string.IsNullOrWhiteSpace(code))
                return OperationResult<bool>.Fail("Mã xác thực không hợp lệ.");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return OperationResult<bool>.Fail("Người dùng không tồn tại.");

            if (!user.EmailConfirmed)
            {
                string decodedCode;

                try
                {
                    decodedCode = Encoding.UTF8.GetString(
                        WebEncoders.Base64UrlDecode(code)
                    );
                }
                catch
                {
                    return OperationResult<bool>.Fail("Mã xác thực không hợp lệ hoặc đã bị thay đổi.");
                }

                var result = await _userManager.ConfirmEmailAsync(user, decodedCode);

                if (!result.Succeeded)
                {
                    return OperationResult<bool>.Fail(
                        string.Join("; ", result.Errors.Select(e => e.Description))
                    );
                }

                return OperationResult<bool>.Ok(
                    true,
                    "Xác nhận email thành công. Bạn có thể đăng nhập ngay bây giờ."
                );
            }

            return OperationResult<bool>.Ok(true, "Email đã được xác nhận trước đó.");
        }

        private string ExtractFirstName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return string.Empty;
            var parts = fullName.Trim().Split(' ');
            return parts.Length > 0 ? parts[0] : fullName;
        }

        private string ExtractLastName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return string.Empty;
            var parts = fullName.Trim().Split(' ');
            return parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : "";
        }

        private async Task RevokeAllUserTokens(string userId)
        {
            var tokens = await _context.AutUserUserTokens
                .Where(x => x.UserId == userId && !x.IsRevoked)
                .ToListAsync();

            foreach (var token in tokens)
            {
                token.Revoke();
            }

            await _context.SaveChangesAsync();
        }


    }
}