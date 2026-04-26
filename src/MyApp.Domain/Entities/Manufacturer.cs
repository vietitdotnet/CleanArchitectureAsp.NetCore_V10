using MyApp.Domain.Core.Models;


namespace MyApp.Domain.Entities
{
    public class Manufacturer : BaseEntity<int>
    {
        public string Name { get; private set; } = null!;
        public string? ShortDescription { get; private set; }
        public string? Website { get; private set; }
        public int CountryId { get; private set; }
        public Country Country { get; private set; } = null!;

        public ICollection<Product> Products { get; private set; } = [];


        private Manufacturer() { }

        public Manufacturer(string name, int countryId, string? description = null, string? website = null)
        {
            Name = name;
            CountryId = countryId;
            ShortDescription = description;
            Website = website;
        }

        public void Update(string name, int countryId, string? description = null, string? website = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");

            if (countryId <= 0)
                throw new ArgumentException("CountryId must be greater than 0");

            Name = name;
            CountryId = countryId;
            ShortDescription = description;
            Website = website;
        }

        public static Manufacturer Create(string name, int countryId, string? description = null, string? website = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");
            if(countryId <= 0)
                throw new ArgumentException("CountryId must be greater than 0");


            return new Manufacturer(name, countryId, description, website);
        }


        public void AssignToCountry(Country country) { 
            if (country == null)
                throw new ArgumentNullException(nameof(country));
            Country = country;
            CountryId = country.Id;
        }


    }
}
