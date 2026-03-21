using MyApp.Domain.Core.Specifications;
using MyApp.Domain.Entities;


namespace MyApp.Domain.Specifications.Categorys
{
    public sealed class CategoryBySlugSpec : BaseSpecification<Category>
    {    
        public CategoryBySlugSpec(string slug) : base()
        {
            Criteria = x => x.Slug == slug;
            ApplyAsNoTracking();

        }
    }
}
