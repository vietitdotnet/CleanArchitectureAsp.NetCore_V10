using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
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
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<AutUserToken> AutUserUserTokens { get; set; }

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
