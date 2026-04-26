using MyApp.Application.Features.Promotions.MapRaws;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Promotions.LogicFeatures
{
    public interface IPromotionStrategy
    {
        bool CanApply(PromotionItemRaw p, DateTimeOffset now);
        decimal Calculate(decimal sellingPrice, PromotionItemRaw p);
    }
}
