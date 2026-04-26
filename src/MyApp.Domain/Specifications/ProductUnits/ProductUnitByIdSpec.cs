using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.ProductUnits
{
    public class ProductUnitByIdSpec : ProductUnitSpec
    {
        public ProductUnitByIdSpec(int id)
        {
            Criteria = p => p.Id == id;
        }
    }
}
