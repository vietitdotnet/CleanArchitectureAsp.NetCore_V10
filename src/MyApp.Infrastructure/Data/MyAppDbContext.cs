using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Entities.Districts;
using MyApp.Infrastructure.Entities.Identity;

namespace MyApp.Infrastructure.Data
{

    public class MyAppDbContext : IdentityDbContext<AppUser>
    {
        /// <summary>
        /// setting
        /// dotnet tool install --global dotnet-ef
        /// Or update
        /// dotnet tool update --global dotnet-ef
        /// Open scr run migrations
        /// dotnet ef migrations add InitialCreate -p MyApp.Infrastructure -s MyApp.WebApi
        /// dotnet ef database update -p MyApp.Infrastructure -s MyApp.WebApi
        /// dotnet ef migrations remove -p MyApp.Infrastructure -s MyApp.WebApi
        /// </summary>
        /// <param name="options"></param>
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options){ }

        public DbSet<AppUser> AutUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<AutUserToken> AutUserUserTokens { get; set; }
        public DbSet<Province> Provinces { get; set; }      
        public DbSet<Commune> Communes { get; set; }
        public DbSet<ProductUnit> ProductUnits { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Promotion> Promotions { get; set; } 
        public DbSet<PromotionItem> PromotionItems { get; set; }
        public DbSet<AdministrativeLevel> AdministrativeLevels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            foreach (var item in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = item.GetTableName();

                if (tableName!.StartsWith("AspNet"))
                {
                    item.SetTableName(tableName.Substring(6));
                }
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyAppDbContext).Assembly);
        }

    }
}
