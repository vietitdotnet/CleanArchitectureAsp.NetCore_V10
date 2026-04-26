using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.Products
{
    public class ProductByIdSpec : ProductSpec
    {
        public ProductByIdSpec(int id)
        {
            Criteria = p => p.Id == id;
        }
    }
}
