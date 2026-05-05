
using MyApp.Application.Features.Products.DTOs;
using MyApp.Application.Features.Products.MapRaws;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappings
{
    public class ProductProfile : BaseProfile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.TextStatus, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.ProductUnits))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));


            CreateMap<Product, ProductLookupDto>();
                

            CreateMap<Product, ProductDetailRaw>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.ProductUnits))
                .ForMember(dest => dest.StatusText, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status));

         
        }
    }
}
