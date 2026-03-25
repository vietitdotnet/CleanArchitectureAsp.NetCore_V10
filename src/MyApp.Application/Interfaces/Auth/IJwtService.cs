using MyApp.Domain.Abstractions;
using System.Security.Claims;

namespace MyApp.Application.Interfaces.Auth
{
    public interface IJwtService
    {

        double GetAccessTokenExpirySeconds();
        string GenerateAccessToken(IAppUserReference user, IList<string> roles);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }

}
