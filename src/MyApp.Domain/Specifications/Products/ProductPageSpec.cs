using MyApp.Domain.Core.Specifications;
using MyApp.Domain.Entities;
using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Specifications.Products
{
    public class ProductPageSpec : BaseSpecification<Product>
    {
        public ProductPageSpec(ProductParameters p)
        {
            // Tránh dùng trong kho lu trữ chung (BaseRepository)
            ApplyPaging((p.PageIndex - 1) * p.PageSize, p.PageSize);
        }
    }
}
