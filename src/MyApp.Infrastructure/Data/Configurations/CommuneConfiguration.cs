using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities.Districts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Data.Configurations
{
    public class CommuneConfig : IEntityTypeConfiguration<Commune>
    {
        public void Configure(EntityTypeBuilder<Commune> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Code).IsUnique();

            builder.Property(x => x.Code).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

            builder.Property(x => x.ProvinceCode).IsRequired();

            builder.HasOne(x => x.Province)
                .WithMany(x => x.Communes)
                .HasForeignKey(x => x.ProvinceCode)
                .HasPrincipalKey(x => x.Code)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.AdministrativeLevel)
                   .WithMany(x => x.Communes)
                   .HasForeignKey(x => x.AdministrativeLevelId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
