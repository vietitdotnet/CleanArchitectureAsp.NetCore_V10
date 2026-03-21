using MyApp.Domain.Core.Models;

namespace MyApp.Domain.Entities
{
    public class Product : BaseEntity<int> , IHasSlug
    {
        private Product() { }

        private Product(string name, string slug, decimal price)
        {
            Name = name.Trim();
            Slug = slug.Trim().ToLowerInvariant();
            Price = price;
            DateCreate = DateTime.UtcNow;
        }

        public string Name { get; private set; } = null!;

        public string Slug { get; private set; } = null!;

        public string? Description { get; private set; }

        public decimal Price { get; private set; }

        public DateTime DateCreate { get; private set; }

        public DateTime? DateUpdate { get; private set; }

        public int? CategoryId { get; private set; }

        public Category? Category { get; private set; }

        public ICollection<OrderProduct> OrderProducts { get; } = [];

        public static Product Create(
            string name,
            string slug,
            decimal price,
            string? description = null,
            int? categoryId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.");

            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentException("Slug is required.");

            if (price < 0)
                throw new ArgumentException("Price cannot be negative.");

            return new Product(name, slug, price)
            {
                Description = description?.Trim(),
                CategoryId = categoryId
            };
        }

        public void Update(
            string name,
            string slug,
            decimal price,
            string? description,
            int? categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");

            if (price < 0)
                throw new ArgumentException("Price cannot be negative.");

            Name = name.Trim();
            Slug = slug.Trim().ToLowerInvariant();
            Price = price;
            Description = description?.Trim();
            CategoryId = categoryId;

            DateUpdate = DateTime.UtcNow;
        }

      
        public void ChangePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("New price must be positive.");

            Price = newPrice;
            DateUpdate = DateTime.UtcNow;
        }


        public void AssignToCategory(int categoryId)
        {
            CategoryId = categoryId;
            DateUpdate = DateTime.UtcNow;
        }
    }

}
