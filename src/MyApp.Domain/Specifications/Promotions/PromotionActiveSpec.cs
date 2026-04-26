using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.Promotions
{
    public class PromotionActiveSpec : PromotionSpec
    {
        public PromotionActiveSpec(DateTimeOffset now)
        {
            Criteria = p => p.IsActive && now >= p.StartDate && now <= p.EndDate;
        }
    }
}
