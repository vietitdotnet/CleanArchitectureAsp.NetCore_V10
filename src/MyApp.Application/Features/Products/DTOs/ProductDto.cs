using MyApp.Domain.Core.Models;
using System.ComponentModel;

namespace MyApp.Application.Features.Products.DTOs
{
    public class ProductDto : BaseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Sku { get; set; } = null!;
        public string? Barcode { get; set; }

        public decimal Price { get; set; }
        public string BrandName { get; set; } = null!;
        public string? CategoryName { get; set; }

        public string? ShortDescription { get; set; }
        public string? Description { get; set; }

        public string? PackingSize { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? DosageForm { get; set; }
        public string? Ingredient { get; set; }

        public ProductDto() { }


    }
}
