using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.ProductUints.Repuests
{
    public class CreateProductUnitRequest
    {
        public string UnitName { get; set; } = null!;
        public string? Barcode { get; set; }

        public decimal SellingPrice { get; set; }
    }
}
