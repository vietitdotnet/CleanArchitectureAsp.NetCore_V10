using MyApp.Application.Features.ProductUints.MapRaws;
using MyApp.Domain.Core.Models;
using MyApp.Domain.Enums;


namespace MyApp.Application.Features.Products.MapRaws
{
    public class ProductDetailRaw : BaseDto
    {
        public string Sku { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Barcode { get; set; }
        public string BrandName { get; set; } = null!;
        public string? CategoryName { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; } = null!;

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public string? PackingSize { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? DosageForm { get; set; }
        public string? Ingredient { get; set; }

        public List<string> Images { get; set; } = [];

        public List<ProductUnitRaw> Units { get; set; } = [];
    }
}
