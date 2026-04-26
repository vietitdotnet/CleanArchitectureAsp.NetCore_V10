using Microsoft.AspNetCore.Identity;
using MyApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Exceptions.Extention
{
    public static class IdentityResultExtensions
    {
        public static void ThrowIfFailed(this IdentityResult result)
        {
            if (result.Succeeded)
                return;

            var errors = result.Errors.ToList();

            throw new IdentityResultException(
                errors.Select(e => e.Description),
                errors.Select(e => e.Code)
            );
        }
    }
}
