using MyApp.Domain.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyApp.Domain.Entities
{
    public class Category : BaseEntity<int>
    {
        private Category() { }

        public string Name { get; private set; } = null!;
        public string NormalizedName { get; private set; } = null!;
        public string Slug { get; private set; } = null!;
        public string? Title { get; private set; }
        public string? Description { get; private set; }
        public int? ParentCategoryId { get; private set; }
        public Category? ParentCategory { get; private set; }
        public bool IsDeleted { get; private set; }
        public ICollection<Category> CategoryChildrens { get; } = [];
        public ICollection<Product> Products { get; } = [];


        private Category(string name, string slug)
        {
            Name = name;
            NormalizedName = name.ToUpperInvariant();
            Slug = slug;
        }

        public static Category Create(
            string name,
            string slug,
            string? title = null,
            string? description = null,
            int? parentCategoryId = null)
        {
            // 1. Validation cơ bản
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");

            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentException("Slug cannot be empty");

            // 2. Chuẩn hóa (Sanitization)
            var cleanName = name.Trim();
           
            ValidateSlug(slug);
            // 3. Khởi tạo
            return new Category(cleanName, slug)
            {
                Title = title?.Trim(),
                Description = description?.Trim(),
                ParentCategoryId = parentCategoryId
            };
        }

        public void Update(string name, string slug, string? title, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");

            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentException("Slug is required");

            Name = name.Trim();
            NormalizedName = name.Trim().ToUpperInvariant();
            Slug = slug.ToLowerInvariant().Replace(" ", "-").Trim();
            Title = title?.Trim();
            Description = description?.Trim();
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");
            Name = name.Trim();
            NormalizedName = name.Trim().ToUpperInvariant();
        }



        public void SoftDelete()
        {
            IsDeleted = true;
        }

        public void AddChild(Category child)
        {
            if (child == null)
                throw new ArgumentNullException(nameof(child));

            CategoryChildrens.Add(child);
            child.ParentCategory = this;
            child.ParentCategoryId = Id;
        }


        public void RemoveChild(Category child)
        {
            if (child == null)
                return;

            CategoryChildrens.Remove(child);
            child.ParentCategory = null;
            child.ParentCategoryId = null;
        }


        private static void ValidateSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentException("Slug cannot be empty");

            // chỉ cho phép a-z, 0-9 và dấu -
            if (!Regex.IsMatch(slug, @"^[a-z0-9]+(-[a-z0-9]+)*$"))
                throw new ArgumentException("Slug format is invalid");
        }
    }


}
