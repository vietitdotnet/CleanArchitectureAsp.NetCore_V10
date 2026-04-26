using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;
using System.Reflection.Emit;

namespace MyApp.Infrastructure.Data.Configurations
{
    public class ProductUnitConfiguration : IEntityTypeConfiguration<ProductUnit>
    {
        public void Configure(EntityTypeBuilder<ProductUnit> builder)
        {

            builder.ToTable("ProductUnits");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.PublicId)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.Barcode)
                .HasMaxLength(20);

            builder.Property(x => x.UnitName)
                .HasMaxLength(100)
                .IsRequired()
                .UseCollation("Vietnamese_CI_AI");
              


            builder.Property(p => p.SellingPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.ConversionRate)
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(p => p.ProductUnits)
                .HasForeignKey(p => p.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);




            //Với mỗi ProductIdchỉ cho phép 1 dòng có IsBaseUnit = 1
            builder.HasIndex(x => x.ProductId)
                .HasFilter("[IsBaseUnit] = 1")
                .IsUnique();



            //index
            builder.HasIndex(p => p.UnitName);
            builder.HasIndex(p => p.Barcode).IsUnique();
            builder.HasIndex(x => x.PublicId).IsUnique();
        }
    }
}
