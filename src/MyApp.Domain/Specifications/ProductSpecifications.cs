using MyApp.Domain.Core.Specifications;
using MyApp.Domain.Entities;

namespace MyApp.Domain.Specifications
{
    public static class ProductSpecifications
    {
        public static BaseSpecification<Product> GetProdcutByIdSpec(int id)
        {
            return new BaseSpecification<Product>(x => x.Id == id);
        }

        public static BaseSpecification<Product> GetProductsWithPage(int pageNumber, int pageSize)
        {
            var spec = new BaseSpecification<Product>(x => true);
            spec.ApplyPaging((pageNumber - 1) * pageSize, pageSize);
            spec.ApplyAsNoTracking();
            return spec;
        }

        
    }
}
