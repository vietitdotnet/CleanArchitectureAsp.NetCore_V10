using MyApp.Domain.Core.Models;
using System.Text.RegularExpressions;

namespace MyApp.Domain.Entities
{
    public class Country : BaseEntity<int>
    {
        public string Code { get; private set; } = null!; // "VN", "US"
        public string Name { get; private set; } = null!;
        public string NormalizedName { get; private set; } = null!;
        public string? FlagIcon { get; private set; }

        // Navigation
        public ICollection<Manufacturer> Manufacturers { get; private set; } = new List<Manufacturer>();

        private Country() { }

        public Country(string code, string name, string? flagIcon = null)
        {
            Code = code;
            Name = name;
            NormalizedName = name.ToUpperInvariant();
            FlagIcon = flagIcon;
        }


        public static Country Create(string code, string name, string? flagIcon = null)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Code cannot be empty");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");
            
            var clearCode = code.Trim().ToUpperInvariant();
            var clearName = name.Trim();

            ValidateCode(clearCode);
            return new Country(clearCode, clearName, flagIcon?.Trim());
        }


        private static void ValidateCode(string code)
        {
        
            if (!Regex.IsMatch(code, @"^[A-Z]{2}$|^[A-Z]{3}$"))
                throw new ArgumentException("Invalid country code");
        }
    
    }
  }
