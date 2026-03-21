

using MyApp.Domain.Extentions;
using MyApp.Domain.Paginations.Parameters;


namespace MyApp.Application.Specifications.Products
{
    public sealed class ProdcutsByIdCategoryWithPageSpec : ProductPageFilterSpec
    {
        public ProdcutsByIdCategoryWithPageSpec(int idCategory ,ProductParameters p) : base(p)
        {  
            Criteria = Criteria.And(x => x.CategoryId == idCategory);
            ApplyAsNoTracking();
        }
    }
}
