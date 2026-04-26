using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.Products
{
    public class ProductByIdsSpec : ProductSpec
    {
        public ProductByIdsSpec(List<int> ids)
        {
            Criteria = p => ids.Contains(p.Id);
        }
    
    }
}
