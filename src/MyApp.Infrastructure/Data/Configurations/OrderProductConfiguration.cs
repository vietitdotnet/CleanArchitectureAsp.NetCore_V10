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
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {

            builder.ToTable("OrderProducts");

            builder.HasKey(op => new { op.OrderId, op.ProductId });

            builder.Property(op => op.Quantity)
                .HasDefaultValue(1);

            builder.Property(op => op.Price)
                .HasPrecision(18, 2);
        }
    }
}
