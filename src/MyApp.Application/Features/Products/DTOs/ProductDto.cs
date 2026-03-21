using MyApp.Domain.Core.Models;

namespace MyApp.Application.Features.Products.DTOs
{
    public class ProductDto : BaseDto
    {
        public int Id { get; private set; }

        public string Slug { get; private set; } = null!;

        public string Name { get; private set; } = null!;

        public string? Description { get; private set; }

        public decimal Price { get; private set; }

        public int? CategoryId { get; private set; }

        public ProductDto() { }


    }
}
