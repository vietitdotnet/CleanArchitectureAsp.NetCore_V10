using MyApp.Domain.Core.Specifications;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.Products
{
    public class ProductBySlugSpec : BaseSpecification<Product>
    {
       public  ProductBySlugSpec(string slug)
       {
            Criteria = p => p.Slug == slug;
       }
    }
}
