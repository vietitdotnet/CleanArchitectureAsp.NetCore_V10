using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyApp.Application.Features.Identity.Requests
{
    public class CreateRoleRequest
    {
        public string Name { get; set; } = null!;
    }
}
