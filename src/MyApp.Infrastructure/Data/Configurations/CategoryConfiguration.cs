using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.ToTable("Categorys");

            builder.HasKey(o => o.Id);

            builder.HasOne(c => c.ParentCategory) // Liên kết cha-con
            .WithMany(c => c.CategoryChildrens)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Property(o => o.Description)
                .HasMaxLength(500);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(p => p.Slug).IsUnique();

        }
    }
}
