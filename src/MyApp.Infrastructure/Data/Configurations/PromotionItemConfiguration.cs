using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Data.Configurations
{
    public class PromotionItemConfiguration : IEntityTypeConfiguration<PromotionItem>
    {
        public void Configure(EntityTypeBuilder<PromotionItem> builder)
        {
            builder.ToTable("PromotionItems");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.PromotionId, x.ProductUnitId })
       .IsUnique();

            // ===== Relationship =====

            builder.Property(p => p.ProductName)
             .IsRequired()
             .HasMaxLength(100);

            builder.Property(x => x.UnitBarcode)
                 .HasMaxLength(20);

            builder.Property(p => p.OriginalPrice)
           .HasPrecision(18, 2);

            builder.Property(p => p.OverrideValue)
            .HasPrecision(18, 2);

            builder.Property(p => p.IsActive)
                   .HasDefaultValue(true);


            // ===== Check Constraints =====

            // 1. Giá gốc tại thời điểm snapshot phải dương
            builder.ToTable(t => t.HasCheckConstraint(
                "CK_PromotionItem_OriginalPrice",
                "[OriginalPrice] > 0"
            ));

            // 2. Logic cho giá trị ghi đè (OverrideValue)
            // - Nếu IsPercentageOverride = true (1) -> Value phải từ 0 đến 100
            // - Nếu IsPercentageOverride = false (0) -> Value phải >= 0
            // - Nếu OverrideValue NULL thì không cần check (dùng giá mặc định của Promotion)
            builder.ToTable(t => t.HasCheckConstraint(
                "CK_PromotionItem_OverrideValue_Logic",
                @"([OverrideValue] IS NULL) OR 
                  ([IsPercentageOverride] = 1 AND [OverrideValue] >= 0 AND [OverrideValue] <= 100) OR 
                  ([IsPercentageOverride] = 0 AND [OverrideValue] >= 0)"
            ));

            // 3. Ràng buộc quan hệ logic: Nếu có OverrideValue thì BẮT BUỘC phải có IsPercentageOverride
            // Tránh trường hợp có số nhưng không biết là % hay Tiền
            builder.ToTable(t => t.HasCheckConstraint(
                "CK_PromotionItem_Override_Incomplete",
                "([OverrideValue] IS NULL AND [IsPercentageOverride] IS NULL) OR ([OverrideValue] IS NOT NULL AND [IsPercentageOverride] IS NOT NULL)"
            ));
            builder
                .HasOne(x => x.Promotion)
                .WithMany(x => x.PromotionItems)
                .HasForeignKey(x => x.PromotionId);

            builder
                .HasOne(x => x.ProductUnit)
                .WithMany(x => x.PromotionItems)
                .HasForeignKey(x => x.ProductUnitId);

            // ===== Index =====
            builder.HasIndex(x => x.ProductUnitId);
        }
    }
}
