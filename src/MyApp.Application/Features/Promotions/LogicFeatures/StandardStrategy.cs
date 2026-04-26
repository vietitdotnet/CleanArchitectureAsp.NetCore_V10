using MyApp.Application.Features.Promotions.MapRaws;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Promotions.LogicFeatures
{
    // Chiến lược cho Promotion thường
    public class StandardStrategy : IPromotionStrategy
    {
        public bool CanApply(PromotionItemRaw p, DateTimeOffset now)
        => p.IsActive && now >= p.StartDate && now <= p.EndDate;

        public decimal Calculate(decimal sellingPrice, PromotionItemRaw p)
        {
            var value = p.OverrideValue ?? p.Value;
            var isPercent = p.IsPercentageOverride ?? p.IsPercentage;

            decimal discount = isPercent ? sellingPrice * (value / 100m) : value;

            if (isPercent && p.MaxDiscountAmount > 0)
                discount = Math.Min(discount, p.MaxDiscountAmount.Value);

            return discount;
        }
    }
}
