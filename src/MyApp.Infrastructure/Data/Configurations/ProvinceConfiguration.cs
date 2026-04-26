using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities.Districts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Data.Configurations
{
    public class ProvinceConfig : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired().HasMaxLength(10);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

            builder.HasOne(x => x.AdministrativeLevel)
                   .WithMany(x => x.Provinces)
                   .HasForeignKey(x => x.AdministrativeLevelId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.Code).IsUnique();
        }
    }

}
