using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Authentications.DTOs
{
    public class AuthUserDto
    {
        public string UserId { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string? FullName { get; private set; }
        public List<string> Roles { get; private set; } = [];
        public string? AccessToken { get; private set; }
        public string? RefreshToken { get; private set; }
        public double? ExpiresIn { get; private set; }

        public string? Device { get; private set; }

        public string? IpAddress { get; private set; }

        public string? UserAgent { get; private set; }

        public static AuthUserDto Create(
            string userId, 
            string email, string? fullName,
            List<string> roles, 
            string? accessToken, 
            string? refreshToken,
            double? expiresIn,
            string? device = null,
            string? ipAddress = null,
            string? userAgent = null)
        {
            return new AuthUserDto
            {
                UserId = userId,
                Email = email,
                FullName = fullName,
                Roles = roles,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Device = device,
                IpAddress = ipAddress,
                UserAgent = userAgent,

                ExpiresIn = expiresIn,

         
            };
        }

    }
}
