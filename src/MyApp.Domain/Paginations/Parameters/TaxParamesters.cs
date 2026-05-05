using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Paginations.Parameters
{
    public class TaxParameters : PagingParameters
    {
        protected override int DefaultPageSize => 10;

        public override void Normalize()
        {
            base.Normalize();
        }
    }
}
