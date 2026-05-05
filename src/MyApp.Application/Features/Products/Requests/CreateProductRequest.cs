using MyApp.Application.Features.ProductUints.Repuests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyApp.Application.Features.Products.Requests
{
    public class CreateProductRequest
    {
        public string? Sku { get; set; }
        public string? Barcode { get; set; }

        public string Name { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public decimal CostPrice { get; set; }
        public decimal BasePrice { get; set; }
        public string PackingSize { get; set; } = null!;
        public string? BrandName { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }

        public string? RegistrationNumber { get; set; }
        public string? DosageForm { get; set; }
        public string? Ingredient { get; set; }

        public string? Benefit { get; set; }

        public int? CategoryId { get; set; }
        public int? ManufacturerId { get; set; }
        public int? TaxId { get; set; }

        public int? BrandId { get; set; }
        public string UnitName { get; set; } = null!;

        public string? UnitBarcode { get; set; }

        public decimal SellingPrice { get; set; }

    }
}
