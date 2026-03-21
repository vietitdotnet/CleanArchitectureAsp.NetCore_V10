using MyApp.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Extentions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        public static int? GetTokenVersion(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(UserClaimTypes.TokenVersion)?.Value;
            if (int.TryParse(claim, out var v))
                return v;

            return null;
        }
    }
}
