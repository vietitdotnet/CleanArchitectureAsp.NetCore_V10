using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Identity.DTOs
{
    public class RoleDto : BaseDto
    {
        public string Id { get;   set; } = null!;

        public string Name { get;   set; } = null!;

        public string[] Claims { get;  set; } = [];

       

    }
}
