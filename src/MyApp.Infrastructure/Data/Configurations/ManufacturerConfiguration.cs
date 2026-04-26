using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Data.Configurations
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.ToTable("Manufacturers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.ShortDescription)
                .HasMaxLength(38)
                .IsRequired(false);

            builder.Property(x => x.Website)
                .HasMaxLength(50)
                .IsRequired(false);

            // ===== Relationship =====

            builder.HasOne(x => x.Country)
                .WithMany(c => c.Manufacturers)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);


            // ===== Index =====
            builder.HasIndex(x => x.Name);
        }
    }
}
