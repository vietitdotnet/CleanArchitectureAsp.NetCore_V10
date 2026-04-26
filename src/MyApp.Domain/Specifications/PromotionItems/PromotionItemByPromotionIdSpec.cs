using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.PromotionItems
{
    public class PromotionItemByPromotionIdSpec : PromotionItemSpec
    {
        public PromotionItemByPromotionIdSpec(int promotionId)
        {
            Criteria = x => x.PromotionId == promotionId;
        }
    
    }
}
