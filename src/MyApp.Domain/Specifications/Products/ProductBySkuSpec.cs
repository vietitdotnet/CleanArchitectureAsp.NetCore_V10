using MyApp.Domain.Core.Specifications;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace MyApp.Domain.Specifications.Products
{
    public class ProductBySkuSpec : BaseSpecification<Product>
    {
        public ProductBySkuSpec(string sku)
        {
            Criteria = p => p.Sku == sku;
        }

    }
}
