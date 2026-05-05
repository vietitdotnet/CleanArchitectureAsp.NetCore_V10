using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200)
                .UseCollation("Vietnamese_CI_AI");

      
            builder.Property(x => x.NormalizedName)
                .IsRequired()
                .HasMaxLength(200);
 
            builder.Property(x => x.Code)
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasMaxLength(380);

            builder.Property(x => x.Website)
                .HasMaxLength(200);

            builder.Property(x => x.Country)
                .HasMaxLength(100);

            builder.Property(x => x.LogoUrl)
                .HasMaxLength(300);

            // Thiết lập unique index cho NormalizedName
            builder.HasIndex(x => x.NormalizedName)
              .IsUnique();

            builder.HasIndex(x => x.Code)
               .IsUnique()
               .HasFilter("[Code] IS NOT NULL");
        }
    }
}
