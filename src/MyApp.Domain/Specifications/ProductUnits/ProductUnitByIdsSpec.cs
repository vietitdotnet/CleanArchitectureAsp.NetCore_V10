using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.ProductUnits
{
    public class ProductUnitByIdsSpec : ProductUnitSpec
    {
        public ProductUnitByIdsSpec(List<int> ids)
        {
            Criteria = p => ids.Contains(p.Id);
        }
    }
}
