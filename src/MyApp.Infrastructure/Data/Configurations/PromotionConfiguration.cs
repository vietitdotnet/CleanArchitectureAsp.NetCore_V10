using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Data.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.PublicId)
              .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Value)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.IsPercentage)
                .IsRequired();

            builder.Property(x => x.MaxDiscountAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);

            builder.Property(x => x.IsActive)
                .IsRequired();

      
            builder.Property(x => x.StartDate)
                .IsRequired()
                .HasColumnType("datetimeoffset");

            builder.Property(x => x.EndDate)
                .IsRequired()
                .HasColumnType("datetimeoffset");


            // ===== Check Constraints =====
            builder.ToTable("Promotions", t =>
            {
                t.HasCheckConstraint(
                    "CK_Promotions_DateRange",
                    "[StartDate] <= [EndDate]"
                );
                t.HasCheckConstraint(
                    "CK_Promotions_Value_Logic",
                    "([IsPercentage] = 1 AND [Value] <= 100 AND [Value] >= 0) OR ([IsPercentage] = 0 AND [Value] >= 0)"
                );
                t.HasCheckConstraint(
                    "CK_Promotions_MaxDiscountAmount_Logic",
                    "([IsPercentage] = 1 AND ([MaxDiscountAmount] IS NULL OR [MaxDiscountAmount] >= 0)) OR ([IsPercentage] = 0 AND [MaxDiscountAmount] IS NULL)"
                );

                /*   t.HasCheckConstraint(
                        "CK_Promotions_MaxDiscountAmount_Logic",
                        "([IsPercentage] = 1 AND [MaxDiscountAmount] IS NOT NULL AND [MaxDiscountAmount] >= 0) OR ([IsPercentage] = 0 AND [MaxDiscountAmount] IS NULL)"
                );*/

                // ===== LimitQuantity & SoldQuantity =====
                t.HasCheckConstraint(
                    "CK_Promotions_Quantity_Logic",
                    @"
                        ([LimitQuantity] IS NULL AND [SoldQuantity] >= 0)
                        OR
                        ([LimitQuantity] IS NOT NULL AND [SoldQuantity] >= 0 AND [SoldQuantity] <= [LimitQuantity])
                     "
                );

                // ===== Daily Time Window =====
                t.HasCheckConstraint(
                    "CK_Promotions_DailyTime_Logic",
                    @"
                        ([DailyStartTime] IS NULL AND [DailyEndTime] IS NULL)
                        OR
                        ([DailyStartTime] IS NOT NULL AND [DailyEndTime] IS NOT NULL AND [DailyStartTime] < [DailyEndTime])
                    "
                );

                t.HasCheckConstraint(
                    "CK_Promotions_FlashSale_Fields",
                    @"
                        ([Type] = 1) -- FlashSale
                        OR
                        ([Type] <> 1 AND 
                            [LimitQuantity] IS NULL AND 
                            [DailyStartTime] IS NULL AND 
                            [DailyEndTime] IS NULL
                        )
                    "
                );


            });


            // ===== Index =====
            builder.HasIndex(x => x.Type)
             .HasDatabaseName("IX_Promotion_Type");
            builder.HasIndex(x => x.StartDate);
            builder.HasIndex(x => x.EndDate);
            builder.HasIndex(x => x.PublicId).IsUnique();
        }
    }
}
