using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.Promotions
{
    public class PromotionByIdWihtTyeSpec : PromotionSpec
    {
        public PromotionByIdWihtTyeSpec(int id, int type)
        {
            Criteria = p => p.Id == id && (int)p.Type == type;
        }
    }
}
