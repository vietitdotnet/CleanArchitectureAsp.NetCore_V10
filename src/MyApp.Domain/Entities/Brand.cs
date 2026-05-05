using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Entities
{
    public class Brand : BaseEntity<int>
    {
        public string Name { get; private set; } = null!;
        public string NormalizedName { get; private set; } = null!;

        public string? Code { get; private set; } // mã nội bộ (optional)
        public string? Description { get; private set; }

        public string? Website { get; private set; }
        public string? Country { get; private set; }

        public string? LogoUrl { get; private set; }

        public bool IsActive { get; private set; } = true;

        // Navigation

        private readonly List<Product> _products = new();
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        private Brand() { }

        public Brand(string name)
        {
            SetName(name);
        }

        public void SetName(string name)
        {
            Name = name.Trim();
            NormalizedName = Normalize(name);
        }

        public void SetDescription(string? description)
        {
            Description = description?.Trim();
        }

        public void SetWebsite(string? website)
        {
            Website = website?.Trim();
        }

        public void SetCountry(string? country)
        {
            Country = country?.Trim();
        }

        public void SetLogo(string? logoUrl)
        {
            LogoUrl = logoUrl;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        private static string Normalize(string input)
        {
            return input.Trim().ToUpperInvariant();
        }
    }
}
