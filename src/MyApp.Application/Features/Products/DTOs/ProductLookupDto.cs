using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Products.DTOs
{
    public class ProductLookupDto : BaseDto
    {
        public int Id { get; set; }
        public string Sku { get; set; } = null!;
        public string? Barcode { get; set; }
        public string Name { get; set; } = null!;
    }
}
