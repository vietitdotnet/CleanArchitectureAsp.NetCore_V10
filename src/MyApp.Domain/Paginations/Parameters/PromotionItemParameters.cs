using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Paginations.Parameters
{
    public class PromotionItemParameters : PagingParameters
    {
        protected override int DefaultPageSize => 5;
        public override void Normalize()
       {
            base.Normalize();
       }
    }
}
