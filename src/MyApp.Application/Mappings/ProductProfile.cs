
using MyApp.Application.Features.Products.DTOs;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappings
{
    public class ProductProfile : BaseProfile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
