using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.Promotions
{
    public class PromotionByIdSpec : PromotionSpec
    {
        public PromotionByIdSpec(int id)
        {
            Criteria = p => p.Id == id;
        }
    }
}
