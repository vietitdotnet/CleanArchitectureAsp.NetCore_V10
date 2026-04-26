using MyApp.Application.Features.Categorys.DTOs;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappings
{
    public class CategoryProfile : BaseProfile
    {
        public CategoryProfile() 
        {
            CreateMap<Category, CategoryDto>();
                
        }
    }
}
