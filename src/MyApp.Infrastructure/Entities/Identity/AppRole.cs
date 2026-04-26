using Microsoft.AspNetCore.Identity;
using MyApp.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Entities.Identity
{
    public class AppRole : IdentityRole, IRoleReference
    {

    }
}
