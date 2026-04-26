using MyApp.Application.Features.ProductUints.DTOs;
using MyApp.Application.Features.ProductUints.MapRaws;
using MyApp.Application.Features.PromotionItems.DTOs;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Mappings
{
    public class ProductUnitProfile : BaseProfile
    {
        public ProductUnitProfile()
        {
            DateTimeOffset nowParam = default;

            CreateMap<ProductUnit, ProductUnitRaw>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                  // Filter sơ bộ (chỉ IsActive)
                .ForMember(dest => dest.PromotionRawItems, opt => opt.MapFrom(src => src.PromotionItems
                .Where(pi => pi.IsActive && pi.Promotion.EndDate >= nowParam)));

            CreateMap<ProductUnit, ProductUnitLookupDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<ProductUnit, ProductUnitDto>()
               .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<ProductUnit,PromotionItemPrefillDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.UnitName))
                .ForMember(dest => dest.UnitBarcode, opt => opt.MapFrom(src => src.Barcode))
                .ForMember(dest => dest.OriginalPrice, opt => opt.MapFrom(src => src.SellingPrice));

        }
    }
}
