using MyApp.Application.Specifications.Products;
using MyApp.Domain.Extentions;
using MyApp.Domain.Paginations.Parameters;


namespace MyApp.Domain.Specifications.Products
{
    public sealed class ProductsByCategorySlugSpec : ProductParametersSpec
    {
        public ProductsByCategorySlugSpec(string slugCategory , ProductParameters p) : base(p)
        {
            Criteria = Criteria.And(x => x.Category!.Slug == slugCategory);

        }
    }
}
