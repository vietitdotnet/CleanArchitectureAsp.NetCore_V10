using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.ProductUints.DTOs
{
    public class ProductUnitLookupDto : BaseDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string UnitName { get; set; } = null!;
        public string FullName => $"{ProductName} - {UnitName}";

    }
}
