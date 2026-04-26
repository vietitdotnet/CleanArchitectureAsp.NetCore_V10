using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;


namespace MyApp.Infrastructure.Data.Configurations
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.ToTable("Medicines");

            builder.HasKey(x => x.Id);

            // ===== Properties =====

            builder.Property(x => x.Dosage)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.Contraindications)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(x => x.MedicineType)
                .IsRequired();

            // ===== Relationship 1-1 =====

            builder.HasOne(x => x.Product)
                .WithOne(p => p.Medicine)
                .HasForeignKey<Medicine>(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===== Constraint =====

            builder.HasIndex(x => x.ProductId)
                .IsUnique(); // đảm bảo 1-1
        }
    
    }
}
