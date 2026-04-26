using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Data.Configurations
{
    public class CountryConfigruration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");

            builder.HasKey(x => x.Id);

            // ===== Properties =====

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false); // "VN", "US"

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.FlagIcon)
                .HasMaxLength(500)
                .IsRequired(false);

            // ===== Index =====

            builder.HasIndex(x => x.Code)
                .IsUnique();

            builder.HasIndex(x => x.Name);

            builder.HasIndex(x => new { x.Name, x.Code })
                                    .IsUnique();

            // ===== Relationship =====

            builder.HasMany(x => x.Manufacturers)
                .WithOne(x => x.Country)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
