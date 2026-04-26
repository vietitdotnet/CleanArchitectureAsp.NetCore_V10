using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Models;

namespace MyApp.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
    
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Slug)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Sku)
                .IsRequired()            
                .HasMaxLength(20);

            builder.Property(p => p.Barcode)   
              .HasMaxLength(20);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100)
                .UseCollation("Vietnamese_CI_AI");
               

            builder.Property(p => p.BrandName)
              .IsRequired()
              .HasMaxLength(150);
         
            builder.Property(p => p.ShortDescription)
                   .HasMaxLength(38);

            builder.Property(p => p.Description)
                 .HasMaxLength(2000);

            builder.Property(p => p.RegistrationNumber)
            .HasMaxLength(20);

            builder.Property(p => p.PackingSize)
            .HasMaxLength(50);
         

            builder.Property(p => p.DosageForm)
             .HasMaxLength(100);

            builder.Property(p => p.Ingredient)
             .HasMaxLength(100);

            builder.Property(p => p.DateCreate)
                 .HasColumnType("datetimeoffset")
                 .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                 .ValueGeneratedOnAdd(); // Thiết lập giá trị mặc định khi tạo mới không quan đến việc có truyền vào hay không

            builder.Property(p => p.DateUpdate)
              .HasColumnType("datetimeoffset");
             

            builder.HasOne(x => x.Category)
               .WithMany(x => x.Products)
               .HasForeignKey(x => x.CategoryId)
                .IsRequired(false)
               .OnDelete(DeleteBehavior.Restrict);

           
            builder.HasOne(x => x.Manufacturer)
             .WithMany(x => x.Products)
             .HasForeignKey(x => x.ManufacturerId)
              .IsRequired(false)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Tax)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.TaxId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.BasePrice)
                .HasPrecision(18, 2);
            builder.Property(p => p.CostPrice)
                .HasPrecision(18, 2);

            //index
            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.BrandName);
            builder.HasIndex(p => p.Slug).IsUnique();
            builder.HasIndex(p => p.Barcode).IsUnique();
            builder.HasIndex(p => p.Sku).IsUnique();

        }
    }
}