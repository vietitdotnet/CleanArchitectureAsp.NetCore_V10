using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.PromotionItems
{
    public class PromotionItemByPromoAndUnitSpec : PromotionItemSpec
    {

        public PromotionItemByPromoAndUnitSpec(int promotionId, int productUnitId)
        {
            Criteria = x => x.PromotionId == promotionId && x.ProductUnitId == productUnitId;
        }

    }
}
