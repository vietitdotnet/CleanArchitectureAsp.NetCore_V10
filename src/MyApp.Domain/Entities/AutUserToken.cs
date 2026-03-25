using MyApp.Domain.Abstractions;
using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Entities
{
    public class AutUserToken : BaseEntity<int>
    {
        public string UserId { get; private set; } = null!;

        public IAppUserReference User { get; private set; } = null!;
        
        public string RefreshToken { get; private set; } = null!;

        public DateTime ExpiryTime { get; private set; }

        public bool IsRevoked { get; private set; }

        public DateTime CreatedAt { get; private set; }

        // Optional: You can include additional properties like Device, IP Address, User Agent, etc. for better tracking and security.
        public string? Device { get; private set; }

        public string? IpAddress { get; private set; }

        public string? UserAgent { get; private set; }

        public static AutUserToken Create
            (string userId,
           
           string refreshToken,
           DateTime expiryTime,
           string? device = null,
           string? ipAddress = null,
           string? userAgent = null

           )
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId is required.");
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentException("RefreshToken is required.");
            
            return new AutUserToken
            {

                UserId = userId,
                RefreshToken = refreshToken.Trim(),
                ExpiryTime = expiryTime,
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false,
                Device = device,
                IpAddress = ipAddress,
                UserAgent = userAgent
            };
        }

        public void Revoke()
        {
            IsRevoked = true;
        }
    }
}
