using MyApp.Application.Features.ProductUints.DTOs;
using MyApp.Application.Features.ProductUints.Views;
using MyApp.Domain.Core.Models;
using System.ComponentModel;

namespace MyApp.Application.Features.Products.DTOs
{
    public class ProductDto : BaseDto
    {
        public int Id { get; set; }
        public string Sku { get; set; } = null!;

        public string? ShortName { get; set; }
        public string? CategoryName { get; set; }
        public string? Barcode { get; set; }
        public string Name { get; set; } = null!;

        public string? PackingSize { get; set; }
        public decimal BasePrice { get; set; }
        public decimal CostPrice { get; set; }
        public string? BrandName { get; set; }
        public string TextStatus { get; set; } = null!;
        public int Status { get; set; }

        public IReadOnlyList<ProductUnitDto> Units { get; set; } = new List<ProductUnitDto>();


    }
}
