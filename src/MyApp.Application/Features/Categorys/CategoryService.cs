using AutoMapper;
using MyApp.Application.Common.Service;
using MyApp.Application.Features.Categorys.DTOs;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Entities;
using MyApp.Domain.Specifications.Categorys;


namespace MyApp.Application.Features.Categorys
{
    public class CategoryService : BaseService , ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, 
                                IMapper mapper,
                                IServiceProvider serviceProvider) 
            : base(unitOfWork, mapper, serviceProvider)
        {

        }

        public async Task<IReadOnlyList<CategoryDto>> GetCategorysAsync(CancellationToken ct = default)
        {
            var spec = new CategorySpec();

            var flatList = await _unitOfWork
                .Repository<Category, int>()
                .ListProjectedAsync<CategoryDto>(spec, ct);

            var lookup = flatList.ToLookup(x => x.ParentCategoryId);

            List<CategoryDto> Build(int? parentId)
            {
                return lookup[parentId]
                    .Select(x =>
                    {
                        x.CategoryChildrens = Build(x.Id);
                        return x;
                    })
                    .ToList();
            }

            return Build(null);

        }

    }
}
