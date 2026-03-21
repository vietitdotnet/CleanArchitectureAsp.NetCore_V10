using MyApp.Domain.Abstractions.Users;
using System.Security.Claims;

namespace MyApp.Application.Abstractions.Services
{
    public interface IJwtService
    {

        double GetAccessTokenExpirySeconds();
        string GenerateAccessToken(IAppUserReference user, IList<string> roles);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }

}
