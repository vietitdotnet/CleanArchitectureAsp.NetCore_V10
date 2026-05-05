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


            builder.ToTable("ProductUnits", t =>
            {
                t.HasCheckConstraint(
                   "CK_ProductUnit_ConversionRate",
                   "[ConversionRate] > 0 AND [ConversionRate] <= 100000000"
               );
                t.HasCheckConstraint(
                   "CK_ProductUnit_SellingPrice",
                   "[SellingPrice] > 0 AND [SellingPrice] <= 10000000000"
               );
            });

            builder
               .HasIndex(x => x.ProductId)
               .HasFilter("[ConversionRate] = 1") // Lọc những dòng có tỉ lệ là 1
               .IsUnique();


            //index
            builder.HasIndex(p => p.UnitName);
            builder.HasIndex(p => p.Barcode).IsUnique();
            builder.HasIndex(x => x.PublicId).IsUnique();
        }
    }
}
