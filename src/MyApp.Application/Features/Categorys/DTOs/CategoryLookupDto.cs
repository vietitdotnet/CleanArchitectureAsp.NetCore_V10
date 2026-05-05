using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Categorys.DTOs
{
    public class CategoryLookupDto : BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
