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


            builder.Property(p => p.PublicId)
                .HasDefaultValueSql("NEWSEQUENTIALID()");


            builder.Property(o => o.Note)
                   .HasMaxLength(500);

            builder.HasMany(o => o.OrderItems)
                    .WithOne(op => op.Order)
                    .HasForeignKey(op => op.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.OrderDate)
                    .HasColumnType("datetimeoffset")
                    .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                    .ValueGeneratedOnAdd(); // Thiết lập giá trị mặc định khi tạo mới không quan đến việc có truyền vào hay không


            builder.Property(p => p.Status)
                   .HasConversion<int>();

            builder.OwnsOne(x => x.Address, a =>
            {
                a.Property(p => p.ProvinceCode)
                    .HasColumnName("ProvinceCode")
                    .HasMaxLength(20)
                    .IsRequired();

                a.Property(p => p.ProvinceName)
                    .HasColumnName("ProvinceName")
                    .HasMaxLength(100)
                    .IsRequired();

                a.Property(p => p.CommuneCode)
                    .HasColumnName("CommuneCode")
                    .HasMaxLength(20)
                    .IsRequired();

                a.Property(p => p.CommuneName)
                    .HasColumnName("CommuneName")
                    .HasMaxLength(100)
                    .IsRequired();

                a.Property(p => p.Detail)
                    .HasColumnName("AddressDetail")
                    .HasMaxLength(255)
                    .IsRequired();
            });

            // ===== AnonCustomer (Value Object) =====
            builder.OwnsOne(x => x.Customer, c =>
            {
                c.Property(p => p.CustomerName)
                    .HasColumnName("CustomerName")
                    .HasMaxLength(100)
                    .IsRequired();

                c.Property(p => p.PhoneNumber)
                    .HasColumnName("PhoneNumber")
                    .HasMaxLength(20)
                    .IsRequired();

                c.Property(p => p.Email)
                    .HasColumnName("CustomerEmail")
                    .HasMaxLength(150);
            });

            builder.HasOne(o => (AppUser)o.User!)
                     .WithMany()
                     .HasForeignKey(o => o.CreatedByUserId)
                     .IsRequired(false)
                     .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(x => x.OrderItems)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);



            // ===== Index (tối ưu query) =====
            builder.HasIndex(x => x.CreatedByUserId);
            builder.HasIndex(x => x.OrderDate);
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.PublicId).IsUnique();


        }

    }
}
