using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.PromotionItems
{
    public class PromotionItemByIdSpec : PromotionItemSpec
    {
        public PromotionItemByIdSpec(int id)
        {
            Criteria = x => x.Id == id;
        }
    }
}
