using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Data.Configurations
{
    public class TaxConfiguration : IEntityTypeConfiguration<Tax>
    {
        public void Configure(EntityTypeBuilder<Tax> builder)
        {
            builder.ToTable("Taxes");

            builder.HasKey(t => t.Id);

            builder.Property(p => p.Percentage)
             .HasPrecision(18, 2);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
            
         

        }
    }
}
