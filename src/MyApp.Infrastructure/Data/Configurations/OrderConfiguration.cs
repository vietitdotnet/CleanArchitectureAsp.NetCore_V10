using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Entities.Identity;

namespace MyApp.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasOne(o => (AppUser)o.User)
                   .WithMany()
                   .HasForeignKey(o => o.CreatedByUserId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);

   

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Description)
                   .HasMaxLength(500);

            builder.Property(o => o.OrderDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasMany(o => o.OrderProducts)
                    .WithOne(op => op.Order)
                    .HasForeignKey(op => op.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Status)
                   .HasConversion<int>();
        }
        
    }
}
