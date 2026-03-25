using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyApp.Application.Interfaces.Auth;
using MyApp.Domain.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace MyApp.Infrastructure.Services.Auth
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly double _accessTokenExpirySeconds;
    
        public JwtService(IConfiguration configuration)
        {
       
          
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            // Lấy giá trị cấu hình từ appsettings.json
            var secret = _configuration["JWT:Secret"]!;
            _issuer = _configuration["JWT:ValidIssuer"]!;
            _audience = _configuration["JWT:ValidAudience"]!;
            _accessTokenExpirySeconds = double.Parse(_configuration["JWT:AccessTokenValidityInSeconds"] ?? "3600");

            if (string.IsNullOrWhiteSpace(secret))
                throw new InvalidOperationException("JWT:Secret is not configured.");

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        }

        public double GetAccessTokenExpirySeconds() => _accessTokenExpirySeconds;



        /// <summary>
        /// Sinh access token chứa các claims cơ bản (user info, roles)
        /// </summary>
        public string GenerateAccessToken(IAppUserReference user, IList<string> roles)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>
            {
               new Claim(JwtRegisteredClaimNames.Sub, user.Id ?? string.Empty),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
               new Claim(ClaimTypes.NameIdentifier, user.Id!),             
               new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty),
               new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty),           
               
            };

            // Thêm roles vào claims
            if (roles != null && roles.Any())
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(_accessTokenExpirySeconds),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Sinh refresh token ngẫu nhiên, an toàn bằng RNGCryptoServiceProvider
        /// </summary>
        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        /// <summary>
        /// Lấy ClaimsPrincipal từ access token đã hết hạn (dùng khi refresh)
        /// </summary>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false, // cho phép đọc token hết hạn
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = _key
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

    }
}
