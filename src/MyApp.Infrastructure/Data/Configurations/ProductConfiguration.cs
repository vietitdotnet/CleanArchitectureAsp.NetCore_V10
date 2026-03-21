using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Models;

namespace MyApp.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Description)
                    .HasMaxLength(1000);

            builder.HasIndex(p => p.Slug).IsUnique();

            builder.Property(p => p.Price)
                .HasPrecision(18, 2);

        }
    }
}