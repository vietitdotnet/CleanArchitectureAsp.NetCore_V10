using MyApp.Domain.Core.Models;

namespace MyApp.Domain.Entities
{
    public class Country : BaseEntity<int>
    {
        public string Code { get; private set; } = null!; // "VN", "US"
        public string Name { get; private set; } = null!;
        public string? FlagIcon { get; private set; }

        // Navigation
        public ICollection<Manufacturer> Manufacturers { get; private set; } = new List<Manufacturer>();

        private Country() { }

        public Country(string code, string name, string? flagIcon = null)
        {
            Code = code;
            Name = name;
            FlagIcon = flagIcon;
        }
    }
}
