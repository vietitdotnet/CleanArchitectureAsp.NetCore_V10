using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UnitName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.UnitBarcode)
               .HasMaxLength(20);

            builder.Property(p => p.ProductName)
              .IsRequired()
              .HasMaxLength(100);

            builder.Property(p => p.ProductSku)
               .IsRequired()
               .HasMaxLength(20);

            builder.Property(x => x.UnitPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.TotalPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.DiscountAmount)
                .HasPrecision(18, 2);

            builder.Property(x => x.VatPercentage)
                .HasPrecision(5, 2);


            // ===== Relationship =====

            builder.HasOne(x => x.ProductUnit)
                .WithMany()
                .HasForeignKey(x => x.ProductUnitId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== Index =====

            builder.HasIndex(x => x.ProductUnitId);
        }
    }

}
