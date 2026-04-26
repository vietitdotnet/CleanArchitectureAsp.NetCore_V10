using MyApp.Domain.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class Category : BaseEntity<int>
    {
        private Category() { }

        public string Name { get; private set; } = null!;
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
            var cleanSlug = slug.ToLowerInvariant().Replace(" ", "-").Trim();

            // 3. Khởi tạo
            return new Category(cleanName, cleanSlug)
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

            Name = name;
            Slug = slug;
            Title = title;
            Description = description;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");
            Name = name.Trim();
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
    }


}
